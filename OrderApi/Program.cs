using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderApi.Consumer;
using OrderApi.Data;
using OrderApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOrder, OrderRepo>();
builder.Services.AddDbContext<OrderDbContext>(o=>o.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProductConsumer>();
    x.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq://localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });
        config.ReceiveEndpoint("product-queue", e =>
        {
            e.ConfigureConsumer<ProductConsumer>(context);
        });
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();    
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();
