using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly HttpClient _client;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(HttpClient client, ILogger<WeatherForecastController> logger)
    {
        _client = client;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public ActionResult<WeatherForecast> Get()
    {
        var rnd = new Random(100);
        
        return DateTime.Now.DayOfWeek switch
        {
            DayOfWeek.Monday => Ok(new WeatherForecast
                {Date = DateTime.Now, TemperatureC = rnd.Next(35), Summary = Summaries[(int) DayOfWeek.Monday]}),
            DayOfWeek.Sunday => Ok(new WeatherForecast
                {Date = DateTime.Now, TemperatureC = rnd.Next(35), Summary = Summaries[(int) DayOfWeek.Sunday]}),
            DayOfWeek.Tuesday => Ok(new WeatherForecast
                {Date = DateTime.Now, TemperatureC = rnd.Next(35), Summary = Summaries[(int) DayOfWeek.Tuesday]}),
            DayOfWeek.Wednesday => Ok(new WeatherForecast
                {Date = DateTime.Now, TemperatureC = rnd.Next(35), Summary = Summaries[(int) DayOfWeek.Wednesday]}),
            DayOfWeek.Thursday => Ok(new WeatherForecast
                {Date = DateTime.Now, TemperatureC = rnd.Next(35), Summary = Summaries[(int) DayOfWeek.Thursday]}),
            DayOfWeek.Friday => Ok(new WeatherForecast
                {Date = DateTime.Now, TemperatureC = rnd.Next(35), Summary = Summaries[(int) DayOfWeek.Friday]}),
            DayOfWeek.Saturday => Ok(new WeatherForecast
                {Date = DateTime.Now, TemperatureC = rnd.Next(35), Summary = Summaries[(int) DayOfWeek.Saturday]}),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [HttpGet("GetFiveDayForecast", Name = "GetFiveDayForecast")]
    public async Task<ActionResult<string>> GetFiveDayForecast()
    {
        var result = await _client.GetAsync("https://raw.githubusercontent.com/storey247/code-snippet/main/weather.json");
        return Ok(result.Content.ReadAsStringAsync());
    }
        
}