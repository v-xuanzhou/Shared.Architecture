using Shared.Architecture.Astro.Pipeline.PipeplineDefinition.SqlParamProider;

namespace Shared.Architecture.Astro.Pipeline.PipeplineDefinition
{
    public class SqlDbAstroTablePipelineStageDefinition : AstroTablePipelingStageDefinition
    {
        public string _sqlText { get; set; }
        public string? _connStringKey { get; set; }
        public ISqlParamProvider _sqlParamProvider { get; set; }
        public SqlDbAstroTablePipelineStageDefinition(string sqlText, string connString, ISqlParamProvider sqlParamProvider)
        {
            _sqlText = sqlText;
            _connStringKey = connString;
            _sqlParamProvider = sqlParamProvider;
        }
    }
}
