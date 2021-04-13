using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TMAS.DB.Models;

namespace TMAS.Providers
{
    public interface IGoogleAuthProvider : IExternalAuthProvider
    {
        Provider Provider { get; }
    }
    public class GoogleAuthProvider<TUser> : IGoogleAuthProvider where TUser : IdentityUser, new()
    {
        public Provider Provider => ProviderDataSource
            .GetProviders()
            .FirstOrDefault(x=>x.Name.ToLower() == ProviderType.Google.ToString().ToLower());

        public JObject GetUserInfo(string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}
