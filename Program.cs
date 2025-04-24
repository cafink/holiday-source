using System.Text.Json;
using Holidays;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapGet("/", async () => {

    string countryBaseUrl = "https://date.nager.at/api/v3/CountryInfo/";
    string holidayBaseUrl = "https://date.nager.at/api/v3/PublicHolidays/2025/";
    HttpClient client = new HttpClient();

    string[] countriesOfInterest = ["US", "BW", "CA"];
    List<CountryWithHolidays> countryList = new List<CountryWithHolidays>();

    for (int i = 0; i < countriesOfInterest.Length; i++)
    {

        // get country name
        HttpResponseMessage countryResponse = await client.GetAsync(countryBaseUrl + countriesOfInterest[i]);
        string countryResponseString = await countryResponse.Content.ReadAsStringAsync();
        CountryWithHolidays country = new CountryWithHolidays(
            JsonSerializer.Deserialize<Dictionary<string, Object>>(
                countryResponseString
            )["commonName"].ToString()
        );

        // get list of holidays
        HttpResponseMessage holidaysResponse = await client.GetAsync(holidayBaseUrl + countriesOfInterest[i]);
        string holidaysResponseString = await holidaysResponse.Content.ReadAsStringAsync();
        country.holidays = JsonSerializer.Deserialize<Holiday[]>(holidaysResponseString);

        countryList.Add(country);
    }

    return JsonSerializer.Serialize(countryList);
});

app.Run();