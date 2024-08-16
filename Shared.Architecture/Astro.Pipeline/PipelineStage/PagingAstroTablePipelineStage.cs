using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage
{
    public class PagingAstroTablePipelineStage:AstroPipelineBaseStage<PagingAstroTablePipelineStageDefinition>
    {
        public override async Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request, IAstroTableRetriever nextPipelineStage)
        {
            var table =  await nextPipelineStage.GetAstroTableAsync(request);
            //table.Columns.Add("paging stage 2");
            return table;
        }
    }
}
