namespace DxBlazorApplication1.Data
{
    public class WeatherForecastService
    {
        public static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 15).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = new List<string>() { Summaries[rng.Next(Summaries.Length - 1)], Summaries[rng.Next(Summaries.Length - 1) + 1] }
            }).ToArray());
        }
    }
}