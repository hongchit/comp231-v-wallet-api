using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Providers
{
    public static class GlobalIntegrationJwtManager
    {
        private static readonly string Secret;

        static GlobalIntegrationJwtManager()
        {
            var hmac = new HMACSHA256();
            Secret = Convert.ToBase64String(hmac.Key);
        }

        public static string GenerateToken(AuthenticateViewModel authenticationModel)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.PrimarySid, authenticationModel.PrimaryId),
                    new Claim(ClaimTypes.Email, authenticationModel.Username),
                    new Claim(ClaimTypes.Name, authenticationModel.Name),
                    new Claim(ClaimTypes.Role, authenticationModel.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest)
            };

            var tokenRawData = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(tokenRawData);
        }

        public static ClaimsPrincipal? GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

                if (jwtToken == null)
                {
                    return null;
                }

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
