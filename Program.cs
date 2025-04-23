var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapGet("/", () => @"[
  { ""name"": ""United States"", ""holidays"": [""Arbor Day"", ""National Donut Day""] },
  { ""name"": ""Botswana"", ""holidays"": [""National Fried Chicken Day"", ""April Fool's Day""] },
  { ""name"": ""Canada"", ""holidays"": [""Flag Day"", ""Boxing Day""] }
]");

app.Run();
