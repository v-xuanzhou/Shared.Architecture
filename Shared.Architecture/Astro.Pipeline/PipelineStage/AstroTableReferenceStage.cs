using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface.MyException;
using Shared.Architecture.Astro.Pipeline.PipelineStage.MidVariable;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;

namespace Shared.Architecture.Astro.Pipeline.PipelineStage
{
    public class AstroTableReferenceStage : AstroPipelineBaseStage<AstroTableReferenceStageDefinition>
    { 
        private readonly IAstroTablePipelineExecuter _astroTablePipelineExecuter;
        private readonly IPipelineContext _context;
        public AstroTableReferenceStage(IAstroTablePipelineExecuter astroTablePipelineExecuter, IPipelineContext context)
        {
            _astroTablePipelineExecuter = astroTablePipelineExecuter;
            _context = context;
        }

        public override async Task<Astrotable> GetAstroTableAsync(GetAstroTableRequest request, IAstroTableRetriever nextPipelineStage)
        {
            ValidateEntityRegistered();
            request.EntityDefinitionName = TypedDefinition.ReferencePipelineEntityName;
            return await _astroTablePipelineExecuter.GetAstroTableAsync(request);
        }

        private void ValidateEntityRegistered()
        {
            if (!_context.IsEntityRegistered(TypedDefinition.ReferencePipelineEntityName))
                throw new AstroException("Entity Name {0} not registered", TypedDefinition.ReferencePipelineEntityName);
        }
    }
}
