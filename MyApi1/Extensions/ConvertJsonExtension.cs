using Newtonsoft.Json;

namespace UserApi.Extensions
{
    public static class ConvertJsonExtension
    {
        public static string Serialize(this object obj)
        {
            return obj == null? null:JsonConvert.SerializeObject(obj);
        }
    }
}
