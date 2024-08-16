using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Architecture.Astro.Pipeline.Internal
{
    public class AstroTableEntityDefinition
    {
        public string Name { get; private set; }
        public IList<AstroTablePipelingStageDefinition> PipelineDefinitions { get; set; }
        public AstroTableEntityDefinition(string name, params AstroTablePipelingStageDefinition[] pipelineDefinitions)
        {
            Name = name;
            PipelineDefinitions = pipelineDefinitions;
        }
    }
}
