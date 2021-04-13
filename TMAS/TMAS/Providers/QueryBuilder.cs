using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMAS.DB.Models.Enums;
namespace TMAS.Providers
{
    public static class QueryBuilder
    {
        public static string FacebookUserInfoQuery(List<string> fields, string token)
        {
            return "?fields=" + String.Join(",", fields) + "&access_token=" + token;
        }
        public static string GetQuery(Dictionary<string, string> values, ProviderType provider)
        {
            switch (provider)
            {
                case ProviderType.Google:
                    var google_access_token = values["token"];
                    return $"?access_token={google_access_token}";
                default:
                    return null;
            }
        }
    }
}
