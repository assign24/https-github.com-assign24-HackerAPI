using Microsoft.Extensions.Configuration;
using Web.Business;
using Web.Business.Intrfaces;
using Web.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
//dependancy injection registration
builder.Services.AddTransient<IHackerNewsStories, HackerNewsStoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
