using System.Text.Json;
using Holidays;
using Microsoft.EntityFrameworkCore;
using HolidaySource.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddHttpClient();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapGet("/", async (ApplicationDbContext context) => {

    string holidayBaseUrl = "https://date.nager.at/api/v3/PublicHolidays/2025/";
    HttpClient client = new HttpClient();

    List<FavoriteCountry> favoriteCountries = await context.FavoriteCountry.ToListAsync();
    List<CountryWithHolidays> countryList = new List<CountryWithHolidays>();

    foreach (FavoriteCountry favoriteCountry in favoriteCountries)
    {
        CountryWithHolidays country = new CountryWithHolidays(favoriteCountry.name);

        // get list of holidays
        HttpResponseMessage holidaysResponse = await client.GetAsync(holidayBaseUrl + favoriteCountry.countryCode);
        string holidaysResponseString = await holidaysResponse.Content.ReadAsStringAsync();
        country.holidays = JsonSerializer.Deserialize<Holiday[]>(holidaysResponseString);

        countryList.Add(country);
    }

    return JsonSerializer.Serialize(countryList);
});

app.Run();