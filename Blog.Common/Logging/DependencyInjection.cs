using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.Logging
{
    public static class DependencyInjection
    {
        public static ConfigureHostBuilder UseCommonLogging(this ConfigureHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
            return hostBuilder;
        }

        public static WebApplication UseCommonLogging(this WebApplication app)
        {
            app.UseSerilogRequestLogging();

            return app;
        }
    }
}
