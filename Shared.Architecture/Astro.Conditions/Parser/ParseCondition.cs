using Newtonsoft.Json.Linq;
using Shared.Architecture.Astro.Conditions.Creater;
using Shared.Architecture.Astro.Conditions.QueryCondition;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;

namespace Shared.Architecture.Astro.Conditions.Parser
{
    public class ParseCondition: IConditionParser
    {
        private readonly Dictionary<string, IQueryConditionCreator> _queryConditionCreators;

        public ParseCondition(IEnumerable<IQueryConditionCreator> queryConditionCreators)
        {
            _queryConditionCreators = queryConditionCreators.ToDictionary(q => q.TokenName);
        }

        public IEnumerable<QueryConditionBase> GetQueryCondition(AstroContextRequest request)
        {
            var queryConditions = new List<QueryConditionBase>();
            foreach (var clause in request.Clauses)
            {
                IQueryConditionCreator creator;
                if (!_queryConditionCreators.TryGetValue(clause.Key, out creator))
                {
                    throw new ArgumentException($"Non supported clause :{clause.Key}");
                }
                queryConditions.AddRange(CreateQuertCondition(creator, clause.Value));
            }
            return queryConditions;
        }

        private IEnumerable<QueryConditionBase> CreateQuertCondition(IQueryConditionCreator creator, JToken clause)
        {
            return creator.Create(clause);
        }
    }
}
