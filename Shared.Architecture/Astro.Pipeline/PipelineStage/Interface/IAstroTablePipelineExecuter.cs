using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage.Interface
{
    public interface IAstroTablePipelineExecuter
    {
        Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request);
    }
}
