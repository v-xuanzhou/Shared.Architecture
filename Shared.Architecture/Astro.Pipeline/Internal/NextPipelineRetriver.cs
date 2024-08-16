using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;

namespace Shared.Architecture.Astro.Pipeline.Internal
{
    public class NextPipelineRetriver : IAstroTableRetriever
    {
        private readonly IList<IAstroTablePipelineStage> _pipelineStages;

        public NextPipelineRetriver(IEnumerable<IAstroTablePipelineStage> pipelineStages)
        {
            _pipelineStages = pipelineStages.ToArray();
        }

        public async Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request)
        {
            Astrotable table;
            IEnumerable<IAstroTablePipelineStage> restStages;
            var nextStage = SplitPipelineSagesIntoFirstAndRest(out restStages);
            table = await nextStage.GetAstroTableAsync(request, new NextPipelineRetriver(restStages));
            return table;
        }

        private IAstroTablePipelineStage SplitPipelineSagesIntoFirstAndRest(out IEnumerable<IAstroTablePipelineStage> restOfStages)
        {
            var firstStage = _pipelineStages.First();
            restOfStages = _pipelineStages.Skip(1);
            return firstStage;
        }
    }
}
