using PrescriptionService;
using PrescriptionService.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("postgres") ?? throw new NullReferenceException("ConnectionString");
var host = builder.Configuration["PostgresHost"] ?? throw new NullReferenceException("PostgresHost");
var port = builder.Configuration["PostgresPort"] ?? throw new NullReferenceException("PostgresPort");
builder.Services.AddSingleton<IPrescriptionRepo>(new DapperPrescriptionRepo(connectionString, host, port));



var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
