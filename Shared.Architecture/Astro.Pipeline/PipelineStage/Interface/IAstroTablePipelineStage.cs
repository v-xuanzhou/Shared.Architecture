using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;


namespace Shared.Architecture.Astro.Pipeline.PipelineStage.Interface
{
    public interface IAstroTablePipelineStage
    {
        AstroTablePipelingStageDefinition Definition { get; set; }
        Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request, IAstroTableRetriever nextPipelineStage);
    }
}
