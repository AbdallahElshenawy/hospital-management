using Hospital_Management.Models;
using Hospital_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Hospital_Management.Authorization
{
    public class ConfirmedAccountRequirement : IAuthorizationRequirement
    {
    }

    public class ConfirmedAccountHandler(IUserServices<ApplicationUser> userServices) : AuthorizationHandler<ConfirmedAccountRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ConfirmedAccountRequirement requirement)
        {
            var user = context.User;

            if (user.Identity.IsAuthenticated)
            {
                var isConfirmed = user.Claims.Any(c => c.Type == ClaimTypes.AuthorizationDecision && c.Value == "True");

                if (isConfirmed)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    var httpContext = context.Resource as HttpContext;
                    if (httpContext != null)
                    {
                        httpContext.Response.Redirect("/User/NotConfirmedAccount");

                        return httpContext.Response.CompleteAsync();
                    }

                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
