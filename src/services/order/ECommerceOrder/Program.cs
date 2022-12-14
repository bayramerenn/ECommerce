using ECommerceCommon.Contants;
using ECommerceCommon.Exceptions;
using ECommerceCommon.Filters;
using ECommerceCommon.Startup_Proj;
using ECommerceOrder.Consumers;
using ECommerceOrder.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
StartupProj.AddSerilog(builder, builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ECommerceContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("ECommerceOrder")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(Program));

#region EventBus

var IsRunningInContainer = bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) && inContainer;
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreateConsumer>();

    x.AddDelayedMessageScheduler();

    x.SetKebabCaseEndpointNameFormatter();

    var entryAssembly = Assembly.GetEntryAssembly();

    x.AddConsumers(entryAssembly);
    x.AddSagaStateMachines(entryAssembly);
    x.AddSagas(entryAssembly);
    x.AddActivities(entryAssembly);

    // By default, sagas are in-memory, but should be changed to a durable
    // saga repository.
    x.SetInMemorySagaRepositoryProvider();

    x.UsingRabbitMq((context, cfg) =>
    {
        if (IsRunningInContainer)
            cfg.Host("rabbitmq");

        cfg.UseDelayedMessageScheduler();

        cfg.ConfigureEndpoints(context);

        cfg.ReceiveEndpoint(EventBusConstans.QueueCreateOrderConsumer, e =>
        {
            e.ConfigureConsumer<OrderCreateConsumer>(context);
        });
    });
});

#endregion EventBus

builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>())
       .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);

builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddTransient<DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var initialiser = services.GetRequiredService<DbInitializer>();

initialiser.Run();

app.Run();