using Newtonsoft.Json.Linq;
using Shared.Architecture.Astro.Conditions.QueryCondition;

namespace Shared.Architecture.Astro.Conditions.Creater
{
    public interface IQueryConditionCreator
    {
        string TokenName { get; }
        IEnumerable<QueryConditionBase> Create(JToken clause);
    }
}
