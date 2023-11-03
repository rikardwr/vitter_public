using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vitter.Utils;

public class AuthenticateAttribute : Attribute, IAsyncPageFilter
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
        PageHandlerExecutionDelegate next)
    {
        if (!context.HttpContext.IsAuthenticated())
        {
            context.HttpContext.Response.StatusCode = 401;
            await context.HttpContext.Response.CompleteAsync();
        }
        else
        {
            await next.Invoke();
        }
    }
}