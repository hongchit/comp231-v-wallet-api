using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Filters;
using v_wallet_api.ViewModels;

namespace v_wallet_api.Providers
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter

    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationResult("Unauthorized", request);
            }

            if (string.IsNullOrEmpty(authorization?.Parameter))
            {
                context.ErrorResult = new AuthenticationResult("Missing Jwt Token", request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            context.Principal = principal;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            if (ValidateToken(token, out AuthenticateViewModel model))
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.PrimarySid, model.PrimaryId),
                    new(ClaimTypes.Email, model.Username),
                    new(ClaimTypes.Name, model.Name),
                    new(ClaimTypes.Role, model.Role)
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal? user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        private bool ValidateToken(string token, out AuthenticateViewModel model)
        {
            model = new AuthenticateViewModel();

            var simplePrinciple = GlobalIntegrationJwtManager.GetPrincipal(token);

            var identity = simplePrinciple?.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return false;
            }

            if (!identity.IsAuthenticated)
            {
                return false;
            }

            var primaryIdClaim = identity.FindFirst(ClaimTypes.PrimarySid);
            model.PrimaryId = primaryIdClaim?.Value;

            var usernameClaim = identity.FindFirst(ClaimTypes.Email);
            model.Username = usernameClaim?.Value;

            var nameClaim = identity.FindFirst(ClaimTypes.Name);
            model.Name = nameClaim?.Value;

            var roleClaim = identity.FindFirst(ClaimTypes.Role);
            model.Role = roleClaim?.Value;

            if (!model.IsValid())
            {
                return false;
            }

            return true;
        }
    }
}
