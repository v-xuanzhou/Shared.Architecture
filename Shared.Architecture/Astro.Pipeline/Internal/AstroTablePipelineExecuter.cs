using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using System.Configuration;

namespace Shared.Architecture.Astro.Pipeline.Internal
{
    public class AstroTablePipelineExecuter : IAstroTablePipelineExecuter
    {
        private readonly IPipelineContext _pipelineContext;

        public AstroTablePipelineExecuter(IPipelineContext pipelineContext)
        {
            _pipelineContext = pipelineContext;
        }

        public async Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request)
        {
            var pipeline = _pipelineContext.GetAstroTablePipeline(request.EntityDefinitionName);

            if (pipeline == null)
            {
                throw new ConfigurationErrorsException(request.EntityDefinitionName + " has no definition registered");
            }

            var PipelineStage = new NextPipelineRetriver(pipeline.PipelineStages);

            var result = await PipelineStage.GetAstroTableAsync(request);
            return result;
        }
    }
}
