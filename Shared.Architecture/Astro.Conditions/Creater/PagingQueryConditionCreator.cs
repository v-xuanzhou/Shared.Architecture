using Newtonsoft.Json.Linq;
using Shared.Architecture.Astro.Conditions.QueryCondition;
using System.Reflection.Metadata.Ecma335;

namespace Shared.Architecture.Astro.Conditions.Creater
{
    public class PagingQueryConditionCreator : IQueryConditionCreator
    {
        private const string PAGE_SIZE = "top";

        private const string PAGE_START = "skip";

        private string TOKEN_NAME = "page";
        public string TokenName => TOKEN_NAME;

        public IEnumerable<QueryConditionBase> Create(JToken clause)
        {
            return new QueryConditionBase[]
            {
                new PagingQueryCondition(clause[PAGE_START].ToObject<int>(), clause[PAGE_SIZE]?.ToObject<int?>())
            };
        }
    }
}
