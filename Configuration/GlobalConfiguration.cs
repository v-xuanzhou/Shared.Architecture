using System.Collections.Specialized;

namespace Configuration
{
    public class GlobalConfiguration
    {
        public static NameValueCollection AppSettings { get; set; } = new NameValueCollection();
        public static NameValueCollection SqlCollections { get; set; } = new NameValueCollection();
    }
}
