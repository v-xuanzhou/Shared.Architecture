

using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using System.Data.SqlClient;

namespace Shared.Architecture.Astro.Pipeline.PipeplineDefinition.SqlParamProider
{
    public interface ISqlParamProvider
    {
        SqlParameter[] GetParameters(GetAstroTableRequest request);
        string[] GetColumnName();
    }
}
