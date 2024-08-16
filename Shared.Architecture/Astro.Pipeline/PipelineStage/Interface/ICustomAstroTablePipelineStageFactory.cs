using Unity;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage.Interface
{
    public interface ICustomAstroTablePipelineStageFactory
    {
        IAstroTablePipelineStage CreatePipelineStage(IUnityContainer conainer);
    }
}
