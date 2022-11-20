using ECommerceBasket.Common;
using ECommerceCommon.Exceptions;
using ECommerceCommon.Filters;
using ECommerceBasket.Consumers;
using ECommerceBasket.Features.Command.Validators;
using ECommerceBasket.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;
using ECommerceCommon.Contants;
using ECommerceCommon.Startup_Proj;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

StartupProj.AddSerilog(builder, builder.Configuration.GetConnectionString("DefaultConnection"));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(Program));

#region redis

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));
builder.Services.AddSingleton<RedisService>(sp =>
{
    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
    var redis = new RedisService(redisSettings.Host, redisSettings.Port);
    redis.Connect();
    return redis;
});

#endregion redis

#region EventBus
 var IsRunningInContainer = bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) && inContainer;
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BasketDeleteConsumer>();

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

        cfg.ReceiveEndpoint(EventBusConstans.QueueDeleteBasketConsumer, e =>
        {
            e.ConfigureConsumer<BasketDeleteConsumer>(context);
        });
    });
});

#endregion EventBus

builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>())
       .ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);

builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();