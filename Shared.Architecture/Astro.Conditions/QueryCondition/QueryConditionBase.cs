namespace Shared.Architecture.Astro.Conditions.QueryCondition
{
    public abstract class QueryConditionBase
    {
        public string Name { get { return GetType().Name; } }
    }
}
