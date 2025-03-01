using Amazon.Runtime;
using Amazon.S3;
using Demo_WebAPI_Access_AWSResources.Services;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
});

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var awsOptions = builder.Configuration.GetSection("AWS").Get<AWSCredentialsOptions>();

builder.Services.AddSingleton(s => {
    var credentials = new BasicAWSCredentials(awsOptions.AccessKey, awsOptions.SecretKey);
    
    var config = new AmazonS3Config
    {
        RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsOptions.Region)
    };

     return new AWSS3Service(credentials, config);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/openapi/v1.json", "Demo API");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

