namespace Shared.Architecture.Astro.Conditions.QueryCondition
{
    public class ColumnFilterQueryCondition : QueryConditionBase
    {
        private string[] _filterColumns;

        public ColumnFilterQueryCondition(params string[] filterColumns)
        {
            _filterColumns = filterColumns;
        }

        public IEnumerable<string> FilterColumns
        {
            get { return _filterColumns; }
            set
            {
                var valuesArray = value.ToArray();
                Array.Sort(valuesArray);
                _filterColumns = valuesArray;
            }
        }
    }
}
