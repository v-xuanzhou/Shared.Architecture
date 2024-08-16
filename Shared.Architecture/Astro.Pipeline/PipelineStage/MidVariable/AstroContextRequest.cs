
using Newtonsoft.Json.Linq;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable
{
    public class AstroContextRequest
    {
        public IDictionary<string, JToken> Clauses { get; set; }
    }
}
