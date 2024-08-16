using Configuration;
using Shared.Architecture.Astro.Conditions.Creater;
using Shared.Architecture.Astro.Conditions.Parser;
using Shared.Architecture.Astro.Pipeline.Internal;
using Shared.Architecture.Astro.Pipeline.PipelineStage;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;
using Unity;
using Unity.Injection;

namespace Shared.Architecture
{
    public class PipelineBootstrap
    {
        public static void Initialize(IUnityContainer _container)
        {

            MapStage<PagingAstroTablePipelineStage, PagingAstroTablePipelineStageDefinition>(_container);
            MapStage<AstroTableReferenceStage, AstroTableReferenceStageDefinition>(_container);
            MapStage<SqlDbAstrotablePipelineStage, SqlDbAstroTablePipelineStageDefinition>(_container);
            MapStage<ResultUnionPipelineStage, ResultUnionPipelineStageDefiniton>(_container);

            _container.RegisterSingleton<IPipelineContext,PipelineContext>();
            _container.RegisterType<IAstroTablePipelineExecuter, AstroTablePipelineExecuter>();
            _container.RegisterType<IAstroTableRetriever, NextPipelineRetriver>();

            InitializeConditionCreator(_container);

        }

        private static void MapStage<TStage, TDefinition>(IUnityContainer container) where TStage : IAstroTablePipelineStage
        {
            var definitionName = typeof(TDefinition).Name;
            if (definitionName == "SqlDbAstroTablePipelineStageDefinition")
            {
                container.RegisterType<IAstroTablePipelineStage, TStage>
                (
                    definitionName,
                    new InjectionConstructor(
                        container.Resolve<AppSettingsKeyValueProvider>()
                    )
                );
            }
            else
            {
                container.RegisterType<IAstroTablePipelineStage, TStage>(definitionName);
            }
        }

        private static void InitializeConditionCreator(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            var queryConditionCreators = new IQueryConditionCreator[]
            {
                new ColumnFilterQueryConditionCreator(),
                new PagingQueryConditionCreator()
            };
            container.RegisterInstance<IEnumerable<IQueryConditionCreator>>(queryConditionCreators);
            container.RegisterType<IConditionParser, ParseCondition>();
        }
    }
}
