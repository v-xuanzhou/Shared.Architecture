using Configuration.inter;
using System.Xml;

namespace Configuration
{
    public class DaoConfigReader: IConfigReader
    {
        private readonly string _basePath;
        private readonly string[] _xmlFiles;

        public DaoConfigReader(string basePath, string[] xmlFiles)
        {
            _basePath = basePath;
            _xmlFiles = xmlFiles;
        }

        public void ReadSettings()
        {
            if (_xmlFiles.Any())
            {
                foreach (var file in _xmlFiles)
                {
                    var path = Path.Join(Directory.GetCurrentDirectory(), _basePath, file);
                    using var xmlReader = XmlReader.Create(path);
                    while (xmlReader.Read())
                    {
                        Read(xmlReader);
                    }
                }
               
            }      
        }

        private void Read(XmlReader xmlReader)
        {
            if (xmlReader.Name == "Sql")
            {
                var key = xmlReader.GetAttribute("key");
                var value = xmlReader.GetAttribute("value");
                if (!string.IsNullOrEmpty(key))
                {
                    GlobalConfiguration.SqlCollections.Add(key, value);
                }
            }
        }
    }
}
