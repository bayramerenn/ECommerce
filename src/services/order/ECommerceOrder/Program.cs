using ECommerceCommon.Contants;
using ECommerceOrder.Consumers;
using ECommerceOrder.Persistence;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ECommerceContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("ECommerceOrder")));


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(Program));

#region EventBus

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreateConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq");

        cfg.ReceiveEndpoint(EventBusConstans.QueueDeleteBasketConsumer, e =>
        {
            e.ConfigureConsumer<OrderCreateConsumer>(context);
        });
    });
});
#endregion EventBus

builder.Services.AddTransient<DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var initialiser = services.GetRequiredService<DbInitializer>();

initialiser.Run();

app.Run();