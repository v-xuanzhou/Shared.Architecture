

namespace Shared.Architecture.Astro.Pipeline.PipeplineDefinition
{
    public class ResultUnionPipelineStageDefiniton:AstroTablePipelingStageDefinition
    {
        public string[] _entityNames { get; set; }
        public ResultUnionPipelineStageDefiniton(params string[] entityNames) 
        {
            _entityNames = entityNames;
        }
    }
}
