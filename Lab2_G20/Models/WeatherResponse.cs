public class WeatherResponse
{
    public Main Main { get; set; }
    public string Name { get; set; }
}

public class Main
{
    public double Temp { get; set; }
}
