using System.ComponentModel.DataAnnotations;

namespace SwaggerTest1
{
    public class WeatherForecast
    {
        /// <summary>
        /// date Of Fire
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [MaxLength(150)]
        [Required]
        public string? Summary { get; set; }
    }
}
