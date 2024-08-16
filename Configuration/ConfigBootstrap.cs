using Configuration.inter;
using Unity;
using Unity.Resolution;

namespace Configuration
{
    public class ConfigBootstrap
    {
        public static void Initialize(IUnityContainer _container)
        {
            _container.RegisterFactory<IEnumerable<IKeyValueProvider>>(
                dc => new List<IKeyValueProvider>
                {   
                    dc.Resolve<AppSettingsKeyValueProvider>(),
                    dc.Resolve<SqlCollectionsKeyValueProvider>()
                });

            _container.RegisterFactory<IEnumerable<IConfigReader>>(
              dc => new List<IConfigReader>
              {
                    dc.Resolve<WebConfigReader>(),
                    dc.Resolve<DaoConfigReader>(
                        new ResolverOverride[]
                        {
                              new ParameterOverride("basePath", "Dao"), 
                              new ParameterOverride("xmlFiles", new []{ "MySql.xml" })  
                        })
              });

        }
    }
}
