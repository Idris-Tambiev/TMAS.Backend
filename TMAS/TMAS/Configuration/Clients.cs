using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace TMAS.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
    {
                new Client {
                 //ClientId = "angular_spa",
                 //ClientName = "Angular SPA",
                 //AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                 //AllowedScopes = { "openid", "profile", "email", "api.read" },
                 //RedirectUris = {"http://localhost:4200/auth-callback"},
                 //PostLogoutRedirectUris = {"http://localhost:4200/"},
                 //AllowedCorsOrigins = {"http://localhost:4200"},
                 //AllowAccessTokensViaBrowser = true,
                 //AccessTokenLifetime = 3600,
                 //RequireClientSecret = false

                    ClientId = "angular_spa",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = false,
                    AllowOfflineAccess = true,
                    AllowedScopes = { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                    "roles",
                    "api.read"},
                     AccessTokenLifetime = 86400,
                    IdentityTokenLifetime = 86400,
                    AbsoluteRefreshTokenLifetime = 86400 * 90,
                    AuthorizationCodeLifetime = 86400 * 90,
                    AccessTokenType = AccessTokenType.Jwt,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = true,
                    Enabled = true,

                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                 }
            };
        }
    }
}
