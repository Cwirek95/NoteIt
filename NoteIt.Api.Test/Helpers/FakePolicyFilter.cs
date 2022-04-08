using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NoteIt.Api.Test.Helpers
{
    public class FakePolicyFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, "storage1")
                }));

            context.HttpContext.User = claimsPrincipal;

            await next();
        }
    }
}
