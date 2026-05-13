using Newsletter.Ai;
using Newsletter.Api.Workers;
using Newsletter.Core;
using Newsletter.Infra;

var builder = WebApplication.CreateBuilder(args);

Configuration.OpenAi.ApiKey =
    builder.Configuration.GetValue<string>("OpenAi:ApiKey") ?? throw new InvalidOperationException("OpenAI API key is not configured.");

builder.Services.AddService();
builder.Services.AddRepositories();
builder.Services.AddNewsletterAi();

builder.Services.AddHostedService<NewsletterWorker>();
    
var app = builder.Build();
Configuration.RootPath = app.Environment.ContentRootPath;

app.MapPost("/", () => "Hello World!");

app.Run();
