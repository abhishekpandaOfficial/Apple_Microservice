using Hangfire;
using HangFire_Email_Job.Services.Email;
using HangFire_Email_Job.Services.Email.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var hangFireConnectionString = builder.Configuration.GetConnectionString("JobConnection");

builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEmailService, EmailService>();

// Configuring Hangfire Client
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180).
    UseSimpleAssemblyNameTypeSerializer().
    UseRecommendedSerializerSettings().
    UseSqlServerStorage(hangFireConnectionString);
});

builder.Services.AddHangfireServer();

// Configuring Hangfire Server

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard();
app.MapHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate(() => Console.WriteLine("Hello From Hangfire"), "* * * * *");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
