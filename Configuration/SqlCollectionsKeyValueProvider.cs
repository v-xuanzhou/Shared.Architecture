using Configuration.inter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class SqlCollectionsKeyValueProvider : IKeyValueProvider
    {
        public string GetValues(string key)
        {
            return key == null ? null : GlobalConfiguration.SqlCollections[key];
        }
    }
}
