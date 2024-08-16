using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage
{
    public class ResultUnionPipelineStage:AstroPipelineBaseStage<ResultUnionPipelineStageDefiniton>
    {
        private readonly IAstroTablePipelineExecuter _astroTablePipelineExecuter;
        private readonly IPipelineContext _pipelineContext;
        public ResultUnionPipelineStage(IAstroTablePipelineExecuter astroTablePipelineExecuter, IPipelineContext pipelineContext)
        {
            _astroTablePipelineExecuter = astroTablePipelineExecuter;
            _pipelineContext = pipelineContext;
        }

        public override async Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request, IAstroTableRetriever nextPipelineStage)
        {     
            var entityNames = TypedDefinition._entityNames;
            var unionTasks = new List<Task<Astrotable>>(entityNames.Length);
            foreach (var entityName in entityNames)
            {
                _pipelineContext.IsEntityRegistered(entityName);
                var taskRequest = request.Clone(entityName, request.QueryConditions);
                unionTasks.Add(_astroTablePipelineExecuter.GetAstroTableAsync(taskRequest));
            }
            await Task.WhenAll(unionTasks).ConfigureAwait(false);

            var resultColumns = unionTasks.SelectMany(col=>col.Result.Columns)
                .GroupBy(col=>col.Name)
                .Select(CoaleseColumnsWithSameName)
                .ToArray();
           
            var resultRows = unionTasks.SelectMany(res => res.Result.Rows); //需要继续去重

            return new Astrotable
            {
                Columns = resultColumns,
                Rows = resultRows.ToList()
            };
        }

        private static AstroTableColumn CoaleseColumnsWithSameName(IEnumerable<AstroTableColumn> astroTableColumns)
        {
            var columns = astroTableColumns as AstroTableColumn[] ?? astroTableColumns.ToArray();
            var first = columns.First().Clone();
            var rest = columns.Skip(1);
            foreach (var column in rest)
            {
                if (column.Name != first.Name)
                    throw new ApplicationException("Internal logic error: All columns must have the same name when coalesced.");
            }
            return first;
        }
    }
}
