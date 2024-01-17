using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuring AWS S3 buckets

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions()); // IT WILL lOAD AWS CONFIGURATION FROM APPSETTINGS.JSON FILE 
builder.Services.AddAWSService<IAmazonS3>(); // It adds the S3 Service into the pipeline 

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
