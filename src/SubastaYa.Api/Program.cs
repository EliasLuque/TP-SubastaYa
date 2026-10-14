using SubastaYa.Api.Workers;
using SubastaYa.Aplication;
using SubastaYa.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration)
    .AddAplication();

// Add Workers
builder.Services.AddHostedService<AuctionClosingWorker>();

var Cors = "Cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors, policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseCors(Cors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
