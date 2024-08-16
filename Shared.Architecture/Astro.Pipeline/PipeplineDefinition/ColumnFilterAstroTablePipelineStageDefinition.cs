using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Architecture.Astro.Pipeline.PipeplineDefinition
{
    public class ColumnFilterAstroTablePipelineStageDefinition : AstroTablePipelingStageDefinition
    {
        public IList<string> ColumnsToInclude { get; set; }

        public ColumnFilterAstroTablePipelineStageDefinition():this(new string[0]) { }

        public ColumnFilterAstroTablePipelineStageDefinition(IList<string> columnsToInclude)
        {
            ColumnsToInclude = columnsToInclude;
        }
    }

}
