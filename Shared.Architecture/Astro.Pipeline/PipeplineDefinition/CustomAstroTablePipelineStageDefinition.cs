using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Unity;

namespace Shared.Architecture.Astro.Pipeline.PipeplineDefinition
{
    public class CustomAstroTablePipelineStageDefinition:AstroTablePipelingStageDefinition, ICustomAstroTablePipelineStageFactory
    {
        public IAstroTablePipelineStage _customPipelineStage { get; }

        public override string Name { get { return _customPipelineStage.GetType().Name; } }

        public CustomAstroTablePipelineStageDefinition(IAstroTablePipelineStage customPipelineStage)
        {
            _customPipelineStage = customPipelineStage; 
        }

        public IAstroTablePipelineStage CreatePipelineStage(IUnityContainer conainer)
        {
            return _customPipelineStage;
        }
    }
}
