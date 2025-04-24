namespace Holidays
{
    public class Holiday
    {
        public DateTime date { get; set; }
        public string name { get; set; }
    }

    public class FavoriteCountry
    {
        public int id { get; set; }
        public string countryCode { get; set; }
        public string name { get; set; }
    }

    public class CountryWithHolidays
    {
        public string name { get; set; }
        public Holiday[] holidays { get; set; }

        public CountryWithHolidays(string name)
        {
            this.name = name;
        }
    }
}