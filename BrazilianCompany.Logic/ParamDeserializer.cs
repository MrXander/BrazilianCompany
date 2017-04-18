#region usings

using Newtonsoft.Json;

#endregion

namespace BrazilianCompany.Logic
{
    internal class ParamDeserializer
    {
        public static T Deserialize<T>(string args)
        {
            return JsonConvert.DeserializeObject<T>(args, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Error
            });
        }
    }
}