using Shared.Architecture.Astro.Conditions.QueryCondition;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using System.Data;
using System.Data.SqlClient;

namespace Shared.Architecture.Astro.Pipeline.PipeplineDefinition.SqlParamProider
{
    public class AstroTableSqlParam
    {
        public SqlDbType sqlDbType {  get; set; }
        public object value {  get; set; }
        public AstroTableSqlParam(SqlDbType sqlDbType, object value)
        {
            this.sqlDbType = sqlDbType;
            this.value = value;
        }
    }

    public class AstroTableSqlParamProvider<TEntity>: ISqlParamProvider
    {  
        private readonly Dictionary<string, AstroTableSqlParam> _parameterMapping;
        private readonly bool IsGetFromRequest = false;
        private readonly string _conditionType;
        private TEntity _entity { get; set; }

        public AstroTableSqlParamProvider() { }

        public AstroTableSqlParamProvider(Dictionary<string, AstroTableSqlParam> parameterMapping)
        {
            _parameterMapping = parameterMapping;
        }

        public AstroTableSqlParamProvider(Dictionary<string, AstroTableSqlParam> parameterMapping, string conditionType)
        {
            _parameterMapping = parameterMapping;
            _conditionType = conditionType;
            IsGetFromRequest = !conditionType.Equals("");
        }

        public string[] GetColumnName()
        {
            Create();
            var columns = new List<string>();
            foreach(var p in _entity.GetType().GetProperties())
            {
                columns.Add(p.Name);
            }
            return columns.ToArray();
        }

        public void Create()
        {
            Type type = typeof(TEntity);
            if (_entity == null)
            _entity = (TEntity)Activator.CreateInstance(type);
        }

        public SqlParameter[] GetParameters(GetAstroTableRequest request)
        {
            var sqlParameters = new List<SqlParameter>();

            if(IsGetFromRequest)
            {
                request.TryExtractSingleQueryCondition(out ColumnFilterQueryCondition qc,  qc =>qc.Name == _conditionType);
            }

            if(_parameterMapping!= null)
            {
                foreach (var pm in _parameterMapping.Keys)
                {
                    var astroTableSqlParam = _parameterMapping[pm];
                    var sqlParameter = new SqlParameter
                    {
                        ParameterName = pm,
                        SqlDbType = astroTableSqlParam.sqlDbType,
                        Value = astroTableSqlParam.value
                    };
                    sqlParameters.Add(sqlParameter);
                }     
            }
            return sqlParameters.ToArray();
        }
    }
}
