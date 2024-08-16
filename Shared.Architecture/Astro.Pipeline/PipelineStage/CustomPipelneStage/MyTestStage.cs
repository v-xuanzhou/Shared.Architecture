using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage.CustomPipelneStage
{
    public class MyTestStage : AstroPipelineBaseStage<CustomAstroTablePipelineStageDefinition>
    {
        public override async Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request, IAstroTableRetriever nextPipelineStage)
        {
            var result = await nextPipelineStage.GetAstroTableAsync(request);
            var list = new List<object>
            {
                "123",123
            };
            result.Rows.Add(list);
            return result;
        }
    }
}
