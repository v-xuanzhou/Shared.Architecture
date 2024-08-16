using Configuration.inter;
using MyApi1.Controllers;
using Shared.Architecture.Astro.Pipeline.PipelineStage.CustomPipelneStage;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition.SqlParamProider;
using System.Data;

namespace UserApi.PipelineRegistrations
{
    public class MyPipelineRegiser: IPipelineRegistrant
    {
        public IKeyValueProvider _keuValueProvider;

        public MyPipelineRegiser(IKeyValueProvider keuValueProvider)
        {
            _keuValueProvider = keuValueProvider;
        }

        public void RegisterPipelines(IPipelineContext pipelineContext)
        {
            RegisterFirstPipeline(pipelineContext);
        }

        public void RegisterFirstPipeline(IPipelineContext pipelineContext)
        {
            pipelineContext.Register(
                "U1",
                new SqlDbAstroTablePipelineStageDefinition(
                    _keuValueProvider.GetValues("U1"),
                    "DbConn",
                    new AstroTableSqlParamProvider<Student>(
                         new Dictionary<string, AstroTableSqlParam>
                         {
                             { "@Age",new AstroTableSqlParam(SqlDbType.Int, 18)}
                         })));

            pipelineContext.Register(
                "U2",
                new CustomAstroTablePipelineStageDefinition(new MyTestStage()),
                new SqlDbAstroTablePipelineStageDefinition(
                    "SELECT * FROM [dbo].[student] WHERE [Age] = @Age",
                    "DbConn",
                    new AstroTableSqlParamProvider<Student>(
                         new Dictionary<string, AstroTableSqlParam>
                         {
                             { "@Age",new AstroTableSqlParam(SqlDbType.Int, 19)}
                         })));

            pipelineContext.Register(
                "U4",
                new SqlDbAstroTablePipelineStageDefinition(
                    "SELECT * FROM [dbo].[student]",
                    "DbConn",
                    new AstroTableSqlParamProvider<Student>()));

            pipelineContext.Register(
                "U3",
                new ResultUnionPipelineStageDefiniton("U1", "U2", "U4")
                );
        }

    }
}
