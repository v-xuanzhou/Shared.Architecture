using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;

namespace UserApi.PipelineRegistrations
{
    public interface IPipelineRegistrant
    {
        void RegisterPipelines(IPipelineContext pipelineContext);
    }
}
