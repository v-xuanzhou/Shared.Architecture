using Newtonsoft.Json.Linq;
using Shared.Architecture.Astro.Conditions.QueryCondition;

namespace Shared.Architecture.Astro.Conditions.Creater
{
    public class ColumnFilterQueryConditionCreator : IQueryConditionCreator
    {
        private const string COLUMNCODE_KEY = "columns";

        private const string TOKEN_NAME = "columnFilter";
        public string TokenName => TOKEN_NAME;

        public IEnumerable<QueryConditionBase> Create(JToken clause)
        {
            return new QueryConditionBase[]
            {
                new ColumnFilterQueryCondition(clause[COLUMNCODE_KEY].ToObject<String[]>())
            };
        }
    }
}
