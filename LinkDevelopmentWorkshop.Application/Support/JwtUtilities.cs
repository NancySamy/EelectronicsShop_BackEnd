using LinkDevelopmentWorkshop.Application.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevelopmentWorkshop.Application.Support
{
    public static class JwtUtilities
    {
        #region statics.


        public static bool IsProcessAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole(CustomRoles.Adminstrator);
        }


        public static bool IsUser(ClaimsPrincipal user)
        {
            return user.IsInRole(CustomRoles.Adminstrator);

        }


        #endregion

        public static JwtSecurityToken ExtractJwtToken(HttpRequest httpRequest)
        {
            try
            {
                var jwt = ExtractJwtEncodedToken(httpRequest);
                if (string.IsNullOrWhiteSpace(jwt))
                {
                    return null;
                }

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                return token;
            }
            catch
            {
                return null;
            }
        }
        public static JwtSecurityToken ExtractJwtToken(string jwt)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jwt))
                {
                    return null;
                }

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);

                return token;
            }
            catch
            {
                return null;
            }
        }

        public static IEnumerable<Claim> GetClaims(HttpContext httpContext, JwtSecurityToken jwt = null)
        {
            var claims = GetEffectiveClaims(httpContext, jwt);
            return claims;
        }

        public static IEnumerable<string> GetClaimValues(string key, HttpContext httpContext, JwtSecurityToken jwt = null)
        {
            var claims = GetEffectiveClaims(httpContext, jwt);
            return claims?.Where(x => x.Type == key)?.Select(x => x.Value).ToList();
        }

        public static string GetClaimValue(string key, HttpContext httpContext, JwtSecurityToken jwt = null)
        {
            var claims = GetEffectiveClaims(httpContext, jwt);
            return claims?.Where(x => x.Type == key)?.FirstOrDefault()?.Value;
        }

        private static IEnumerable<Claim> GetEffectiveClaims(HttpContext httpContext, JwtSecurityToken jwt)
        {
            var nativeClaims = httpContext?.User?.Claims;
            var extractClaims = jwt?.Claims;

            return nativeClaims != null && nativeClaims.Any()
                 ? nativeClaims
                 : extractClaims;
        }

        public static string ExtractJwtEncodedToken(HttpRequest httpRequest)
        {
            try
            {
                var jwtNative = httpRequest?.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrWhiteSpace(jwtNative))
                {
                    return null;
                }

                jwtNative = jwtNative.Replace("Bearer ", "");

                return jwtNative;
            }
            catch
            {
                return null;
            }
        }
        public static string ExtractJwtEncodedToken(JwtSecurityToken jwt)
        {
            return jwt?.RawHeader;
        }
        public static string GenerateJwtEncodedToken(string privateKey = "PrivateKey",
                                                     string name = "system.component",
                                                     string email = "",
                                                     string role = "system",
                                                     string issuer = "self",
                                                     string audiance = "system",
                                                     double expiresInMinutes = 60 * 24,
                                                     IEnumerable<Claim> customClaims = null)
        {
            // ...
            var now = DateTime.UtcNow;
            var symmetricKey = privateKey?.GetBytes();
            var subject = new ClaimsIdentity(new Claim[]
                          {
                              new Claim(ClaimTypes.Name, name),
                              new Claim(ClaimTypes.Email, email),
                              new Claim(ClaimTypes.Role, role),
                          });
            if (customClaims != null && customClaims.Any())
            {
                subject.AddClaims(customClaims);
            }

            // ...
            var handler = new JwtSecurityTokenHandler();
            return handler.CreateEncodedJwt(new SecurityTokenDescriptor()
            {
                Subject = subject,
                Issuer = issuer,
                Audience = audiance,
                IssuedAt = now,
                Expires = now.AddMinutes(expiresInMinutes),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature),
            });
        }


        private static byte[] GetBytes(this string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
