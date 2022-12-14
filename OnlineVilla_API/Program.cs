using OnlineVilla_API.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuring SeriLog---------------
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    .WriteTo.File("logs/VillaLogs.txt",rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();
//----------------------------------

//builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddSingleton<ILogging, LoggingV2>();

builder.Services.AddControllers(option => 
{
    //option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
