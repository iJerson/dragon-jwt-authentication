using DragonOAuth.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace DragonOAuth.Providers
{
    public class DragonProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            User _user = await Task.Run(() => {
                //FIND USER
                return new User { UserId = 1 };
            });
          
            if (_user.UserId > 0)
            {
                var identity = new ClaimsIdentity("JWT");

                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim("sub", context.UserName));

                foreach (UserRole __role in _user.Roles)
                    identity.AddClaim(new Claim(ClaimTypes.Role, __role.RoleName));

                var ticket = new AuthenticationTicket(identity, null);

                context.Validated(ticket);
            }
            else
            {
                context.Rejected();
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
        }        
    }
}