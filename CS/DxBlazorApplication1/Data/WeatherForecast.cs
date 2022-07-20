
using System;
using System.Collections.Generic;

namespace DxBlazorApplication1.Data
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public List<string>? Summary { get; set; }
        public string SummaryString => string.Join(",", Summary);
    }
}