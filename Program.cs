using Cleipnir.Flows;
using Cleipnir.Flows.AspNet;
using Cleipnir.Flows.SqlServer;
using Cleipnir.ResilientFunctions.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connStr = "Server=localhost;Database=helloworldflows;User Id=sa;Password=Pa55word!;Encrypt=True;TrustServerCertificate=True;"; // <- replace with your connection string
//todo can be used to clear existing data in database: await DatabaseHelper.RecreateDatabase(connStr);
await DatabaseHelper.CreateDatabaseIfNotExists(connStr);
builder.Services.AddFlows(c => c
    .UseSqlServerStore(connStr)
    .WithOptions(sp => new Options(
            unhandledExceptionHandler: e => sp.GetRequiredService<ILogger>().LogError(e, "Unhandled framework exception"),
            watchdogCheckFrequency: TimeSpan.FromSeconds(5)
        )
    )
    .RegisterFlowsAutomatically()
);

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
