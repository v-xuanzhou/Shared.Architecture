using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface.MyException;
using Unity;

namespace Shared.Architecture.Astro.Pipeline.Internal
{
    public class AstroTablePipeline
    {
        private readonly IUnityContainer _unituContainer;
        public AstroTableEntityDefinition EntityDefinition { get; private set; }
        public IList<IAstroTablePipelineStage> PipelineStages { get; private set; }
       
        public AstroTablePipeline(AstroTableEntityDefinition entityDefinition, IUnityContainer unituContainer)
        {
            _unituContainer = unituContainer;
            EntityDefinition = entityDefinition;
            CreatePipelineStagesFromDefinitions(entityDefinition);
        }

        private void CreatePipelineStagesFromDefinitions(AstroTableEntityDefinition entityDefinition)
        {
            var pipelineStages = new List<IAstroTablePipelineStage>(entityDefinition.PipelineDefinitions.Count());
            foreach (var astroTablePipelineStageDefinition in entityDefinition.PipelineDefinitions)
            {
                var customStageFactory = astroTablePipelineStageDefinition as ICustomAstroTablePipelineStageFactory;
                IAstroTablePipelineStage stage;
                try
                {
                    if (customStageFactory != null)
                    {
                        stage = customStageFactory.CreatePipelineStage(_unituContainer);
                        stage.Definition = astroTablePipelineStageDefinition;
                    }
                    else
                    {
                        stage = _unituContainer.Resolve<IAstroTablePipelineStage>(astroTablePipelineStageDefinition.Name);
                        stage.Definition = astroTablePipelineStageDefinition;
                    }
                }
                catch (ResolutionFailedException ex)
                {
                    throw new AstroException("Unknown pipeline stage definition " + astroTablePipelineStageDefinition.Name, ex);
                }
                pipelineStages.Add(stage);
            }
            PipelineStages = pipelineStages;
        }
    }
}
