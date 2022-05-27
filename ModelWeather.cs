
public class ModelWeather
{
    public string? Date { get; set; }
    public string? Place { get; set; }
    public int? TemperatureF { get; set; }
     public int? Wind { get; set; }

    public string? TemperatureC => (Convert.ToInt16(TemperatureF - 273.15)).ToString();
      
    // public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
    

    public static string ToString(ModelWeather model)
  {
    
return $"{model.Date} {model.Place}  {model.TemperatureF} {model.Summary}";
  }


}
