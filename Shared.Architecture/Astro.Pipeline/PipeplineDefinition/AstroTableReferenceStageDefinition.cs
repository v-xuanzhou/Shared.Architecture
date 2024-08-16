using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Architecture.Astro.Pipeline.PipeplineDefinition
{
    public class AstroTableReferenceStageDefinition:AstroTablePipelingStageDefinition
    {
        public string ReferencePipelineEntityName { get;  set; }
        public AstroTableReferenceStageDefinition(string referencePipelineEntityName)
        {
            ReferencePipelineEntityName = referencePipelineEntityName;
        }
    }
}
