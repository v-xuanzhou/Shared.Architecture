
using Shared.Architecture.Astro.Conditions.QueryCondition;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;

namespace Shared.Architecture.Astro.Conditions.Parser
{
    public interface IConditionParser
    {
        public IEnumerable<QueryConditionBase> GetQueryCondition(AstroContextRequest request);
    }
}
