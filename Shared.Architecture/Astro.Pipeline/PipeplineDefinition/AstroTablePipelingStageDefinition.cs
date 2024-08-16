
namespace Shared.Architecture.Astro.Pipeline.PipeplineDefinition
{
    public class AstroTablePipelingStageDefinition
    {
        public virtual string Name { get { return GetType().Name; } }   
        public virtual IEnumerable<string> DependentEntityDefinitionNames { get { return Enumerable.Empty<string>(); } }
    }
}
