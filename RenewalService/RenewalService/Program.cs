using RenewalService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRenewalService>(new RestSharpRenewalService(builder.Configuration["PrescriptionEndpoint"], builder.Configuration["NotificationEndpoint"]));
builder.Services.AddCronJob<RenewalJob>( renewal =>
{
    renewal.CronExpression = @"*/15 * * * *";
    renewal.TimeZoneInfo = TimeZoneInfo.Utc;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
