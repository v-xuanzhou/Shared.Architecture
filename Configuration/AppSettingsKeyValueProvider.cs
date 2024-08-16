using Configuration.inter;

namespace Configuration
{
    public class AppSettingsKeyValueProvider : IKeyValueProvider
    {
        public string GetValues(string key)
        {
            return key == null ? null : GlobalConfiguration.AppSettings[key];
        }
    }
}
