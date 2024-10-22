public class WeatherResponse
{
    public Main Main { get; set; }
    public string Name { get; set; } // City name
}

public class Main
{
    public double Temp { get; set; } // Temperature
}
