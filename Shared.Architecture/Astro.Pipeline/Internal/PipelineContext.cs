

using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface.MyException;
using Shared.Architecture.Astro.Pipeline.PipeplineDefinition;
using Unity;

namespace Shared.Architecture.Astro.Pipeline.Internal
{
    public class PipelineContext: IPipelineContext
    {
        private readonly Dictionary<string,AstroTablePipeline> _astroTableEntityDefinitions = new Dictionary<string, AstroTablePipeline> ();
        private readonly IUnityContainer _unityContainer;

        public PipelineContext(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Register(string entityDefinitionName, params AstroTablePipelingStageDefinition[] stageDefinitions)
        {
            if (_astroTableEntityDefinitions.ContainsKey(entityDefinitionName))
            {
                throw new AstroException(entityDefinitionName + " has already been registered");
            }
            var definition = new AstroTableEntityDefinition(entityDefinitionName, stageDefinitions);
            _astroTableEntityDefinitions[entityDefinitionName] = new AstroTablePipeline(definition, _unityContainer);
        }

        public AstroTablePipeline GetAstroTablePipeline(string entityDefinitionName)
        {
            AstroTablePipeline pipeline;
             _astroTableEntityDefinitions.TryGetValue(entityDefinitionName, out pipeline);
            return pipeline;
        }

        public bool IsEntityRegistered(string entityName)
        {
            return _astroTableEntityDefinitions.ContainsKey(entityName);
        }
    }
}
