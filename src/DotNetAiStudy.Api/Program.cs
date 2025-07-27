using DotNetAiStudy.Api.Extensions;
using DotNetAiStudy.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddOpenAI();

builder.Services.AddSingleton<ChatService>();
builder.Services.AddSingleton<RecipeService>();
builder.Services.AddSingleton<ImageService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = ".NET AI Study API",
            Version = "v1",
            Description = """  
               This API provides AI-based features such as chat, image generation,  
               recipe creation and audio transcription.  
               """,
            Contact = new()
            {
                Name = "Paulo Alves",
                Email = "pj38alves@gmail.com",
                Url = new Uri("https://github.com/PauloAlves8039")
            },
            License = new()
            {
                Name = "License",
                Url = new Uri("https://github.com/PauloAlves8039")
            },
            TermsOfService = new Uri("https://github.com/PauloAlves8039")
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowAny");

app.UseAuthorization();

app.MapControllers();

app.Run();
