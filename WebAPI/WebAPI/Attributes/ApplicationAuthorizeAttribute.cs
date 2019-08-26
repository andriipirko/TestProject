using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Attributes
{
    public class ApplicationAuthorizeAttribute : AuthorizeAttribute
    {
        public ApplicationAuthorizeAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
