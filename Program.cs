using Cleipnir.Flows;
using Cleipnir.Flows.SqlServer;
using Cleipnir.ResilientFunctions.SqlServer;
using Test.Flows;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connStr = "Server=localhost;Database=flows;User Id=sa;Password=Pa55word!;Encrypt=True;TrustServerCertificate=True;"; // <- replace with your connection string
//todo can be used to clear existing data in database: await DatabaseHelper.RecreateDatabase(connStr);
builder.Services.UseFlows(connStr, provider => new Options(
    unhandledExceptionHandler: e => provider.GetRequiredService<ILogger>().LogError(e, "Unhandled framework exception"),
    crashedCheckFrequency: TimeSpan.FromSeconds(5)
));
builder.Services.AddTransient<HelloWorldFlow>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
