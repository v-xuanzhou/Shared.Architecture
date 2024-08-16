using Shared.Architecture.Astro.Conditions.QueryCondition;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface.MyException;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable
{
    public class GetAstroTableRequest
    {
        public string EntityDefinitionName { get;  set; }

        public List<QueryConditionBase> QueryConditions { get; set; }

        public GetAstroTableRequest() { }

        public GetAstroTableRequest(string entityDefinitionName, List<QueryConditionBase> queryConditions)
        {
            EntityDefinitionName = entityDefinitionName;
            QueryConditions = queryConditions;
        }

        public GetAstroTableRequest TryExtractSingleQueryCondition<T> (out T extractedCondition ,Func<T, bool> predicate) where T :  QueryConditionBase
        {
            var foundCondition =  FindCondition(QueryConditions, predicate);
            extractedCondition = foundCondition;
            var newRequest = new GetAstroTableRequest(EntityDefinitionName, QueryConditions);
            return newRequest;
        }

        public T FindCondition<T>(IEnumerable<QueryConditionBase> conditions, Func<T, bool> predicate) where T : QueryConditionBase
        {
            var queryConditions = conditions.OfType<T>().Where(predicate).ToArray();
            if (queryConditions.Count() > 1)
                throw new AstroException("TryExtractSingleQueryCondition: Can't have more than one " + typeof(T).Name);
            var foundCondition = queryConditions.FirstOrDefault();
            return foundCondition;
        }

        public GetAstroTableRequest Clone(string entityDefinitionName, List<QueryConditionBase> newConditions)
        {
            return new GetAstroTableRequest
            {
                EntityDefinitionName = entityDefinitionName,
                QueryConditions = newConditions
            };
        }
    }
}
