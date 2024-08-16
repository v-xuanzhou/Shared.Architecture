using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface.MyException;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage
{
    public class AstroPipelineBaseStage<TDefinition> : IAstroTablePipelineStage where TDefinition : AstroTablePipelingStageDefinition
    {
        private TDefinition _definition;

        protected TDefinition TypedDefinition => _definition;

        public AstroTablePipelingStageDefinition Definition
        {
            get
            {
                return _definition;
            }
            set
            {
                var definition = value as TDefinition;
                if (definition == null)
                {
                    throw new AstroException("Definition is {0} but should be a {1}", value?.GetType().Name ?? "NULL", typeof(TDefinition).Name);
                }
                _definition = definition;
            }
        }

        public virtual Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request, IAstroTableRetriever nextPipelineStage)
        {
            return nextPipelineStage.GetAstroTableAsync(request);
        }
    }
}
