using System.ComponentModel.DataAnnotations;

namespace SwaggerTest1
{
    /// <summary>
    /// Weather Forecast
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// date Of Fire
        /// </summary>
        [Required]
        public DateOnly Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int TemperatureC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(150)]
        [Required]
        public string? Summary { get; set; }
    }
}
