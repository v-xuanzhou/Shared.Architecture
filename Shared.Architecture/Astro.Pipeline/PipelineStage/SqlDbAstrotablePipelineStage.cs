using Configuration.inter;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;
using System.Data;
using System.Data.SqlClient;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage
{
    public class SqlDbAstrotablePipelineStage : AstroPipelineBaseStage<SqlDbAstroTablePipelineStageDefinition>
    {
        private readonly IKeyValueProvider _keyValueProvider;

        public SqlDbAstrotablePipelineStage(IKeyValueProvider keyValueProvider)
        {
            _keyValueProvider = keyValueProvider;
        }

        public override async Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request, IAstroTableRetriever nextPipelineStage)
        {
            var sqlParamProvider = TypedDefinition._sqlParamProvider;
            if (sqlParamProvider == null)
            {
                throw new ArgumentException("sqlParamProvider should correct be initialized");
            }
            var sqlParams = sqlParamProvider.GetParameters(request);
            var columnNames = sqlParamProvider.GetColumnName();
            var connStringKey = TypedDefinition._connStringKey;
            var table = await ExecuteQueriesAsync(sqlParams, TypedDefinition._sqlText, columnNames, _keyValueProvider.GetValues(connStringKey));
            return table;
        }

        public async Task<Astrotable> ExecuteQueriesAsync(SqlParameter[] sqlParam, string sqlText, string[] dbColumnNames, string connString)
        {
            var list = new List<object>();
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = PrepareSqlCommand(sqlText,conn, sqlParam))
                {
                    using (var dataReader = await cmd.ExecuteReaderAsync())
                    {
                        while (dataReader.Read())
                        {
                           var stageList = new List<object>();
                            foreach(var name in dbColumnNames)
                            {
                                stageList.Add(dataReader.GetValue(name));
                            }
                            list.Add(stageList);
                        }
                    }
                }
                conn.Close();
            }
            return new Astrotable()
            {
                Rows = list,
                Columns = GetAstroTableColumns(dbColumnNames)
            }; 
        }

        private AstroTableColumn[] GetAstroTableColumns(string[] dbColumnNames)
        {
            var astroTableColumns = new List<AstroTableColumn>();
            foreach(var columnName in dbColumnNames)
            {
                astroTableColumns.Add(new AstroTableColumn(columnName));
            }
            return astroTableColumns.ToArray();
        }

        private SqlCommand PrepareSqlCommand(string sqlText,SqlConnection sqlConnection, SqlParameter[] sqlParameters)
        {
            SqlCommand sqlCommand = new SqlCommand(sqlText,sqlConnection);
            foreach(var sqlParameter in sqlParameters)
            {
                if(sqlParameter.Value == null)
                {
                    sqlParameter.Value = DBNull.Value;
                }
                sqlCommand.Parameters.Add(sqlParameter);
            }
            return sqlCommand;
        }

    }
}
