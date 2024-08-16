using Configuration.inter;
using System.Xml;

namespace Configuration
{
    public class WebConfigReader:IConfigReader
    {
        private readonly string _pathString = Path.Join(Directory.GetCurrentDirectory(), "WebXml", "Web.config");
        private bool _inAppSeting = false;

        public void ReadSettings()
        {
            using var xmlReader = XmlReader.Create(_pathString);
            while (xmlReader.Read())
            {
                switch (xmlReader.Name)
                {
                    case "appSettings":
                        InAppsettings();
                        break;
                }

                if (_inAppSeting)
                {
                    ReadAppSettings(xmlReader);
                }
            }
        }

        private void InAppsettings()
        {
            _inAppSeting = true;
        }

        private void ReadAppSettings(XmlReader xmlReader)
        {
            if (xmlReader.Name == "add")
            {
                var value = xmlReader.GetAttribute("value");
                var key = xmlReader.GetAttribute("key");
                if (!string.IsNullOrEmpty(key))
                {
                    GlobalConfiguration.AppSettings.Add(key, value);
                }
            }
        }

    }
}
