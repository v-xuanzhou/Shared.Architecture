namespace Shared.Architecture.Astro.Conditions.QueryCondition
{
    public class PagingQueryCondition: QueryConditionBase
    {
        public int StartRowNum { get; set; }    
        public int? RowCount { get; set; }

        public PagingQueryCondition(int startRowNum, int? rowCount)
        {
            StartRowNum = startRowNum;
            RowCount = rowCount;
        }   
    }
}
