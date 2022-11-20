using Microsoft.AspNetCore.Builder;
using Serilog;

namespace ECommerceCommon.Startup_Proj
{
    public static class StartupProj
    {
        public static void AddSerilog(WebApplicationBuilder builder,string connectionString)
        {
            builder.Host.UseSerilog((hostingContext, loggerConfig) =>
            loggerConfig
            .WriteTo.MSSqlServer(
                connectionString: connectionString,
                tableName: "Logs",
                schemaName: "dbo",
                autoCreateSqlTable: true,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
                )
            );
        }
    }
}