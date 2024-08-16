
using Configuration;
using Configuration.inter;
using Shared.Architecture;
using Shared.Architecture.Astro.Pipeline.PipelineStage.Interface;
using Unity;
using Unity.Injection;
using UserApi.Msic.Auth;
using UserApi.PipelineRegistrations;

namespace MyAPI.Common
{
    public class DependencyResolver
    {
        private readonly IUnityContainer  _container;
        
        public DependencyResolver() : this(new UnityContainer()) { }
        public DependencyResolver(IUnityContainer unityContainer)
        {
            _container = unityContainer;
            reigistBasicDependency();
            ReadConfigSetings();
            ResolveApiDependency();
            registCustomerPipeline();
        }

        /// <summary>
        /// Changing the boot sequence is prohibited
        /// </summary>
        public void reigistBasicDependency()
        {
            ConfigBootstrap.Initialize(_container);
            PipelineBootstrap.Initialize(_container);
        }

        public void ReadConfigSetings()
        {
            var readers = _container.Resolve<IEnumerable<IConfigReader>>();
            foreach (var reader in readers)
            {
                reader.ReadSettings();
            }
        }

        private void ResolveApiDependency()
        {
            _container.RegisterType<IPipelineRegistrant, MyPipelineRegiser>(
            new InjectionConstructor
            (
                _container.Resolve<SqlCollectionsKeyValueProvider>()
            ));

            _container.RegisterInstance(typeof(JwtHelper));

        }

        public void registCustomerPipeline()
        {
            var pipelineContext = _container.Resolve<IPipelineContext>();
            var pipelines = _container.Resolve<IEnumerable<IPipelineRegistrant>>();
            foreach(var pipeline in pipelines)
            {
                pipeline.RegisterPipelines(pipelineContext);
            }
        }
    }
}
