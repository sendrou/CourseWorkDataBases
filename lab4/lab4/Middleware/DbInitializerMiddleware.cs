﻿using Lab02;
using lab4;

namespace lab4.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, Lab1Context dbContext)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                DbInitilializer.Initialize(dbContext);
                context.Session.SetString("starting", "Yes");
            }

            return _next.Invoke(context);
        }
    }

    public static class DbInitializerExtensensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializerMiddleware>();
        }
    }
}
