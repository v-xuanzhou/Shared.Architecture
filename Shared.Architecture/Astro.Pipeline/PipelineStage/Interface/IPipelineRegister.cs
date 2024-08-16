using Shared.Architecture.Astro.Pipeline.Internal;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;


namespace Shared.Architecture.Astro.Pipeline.PipelineStage.Interface
{
    public interface IPipelineContext
    {
        void Register(string pipelineName, params AstroTablePipelingStageDefinition[] stageDefinitions);
        AstroTablePipeline GetAstroTablePipeline(string entityDefinitionName);
        bool IsEntityRegistered(string entityName);
        
    }
}
