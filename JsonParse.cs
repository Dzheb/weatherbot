using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public struct JsonParse
{
    static string? json;

    public static void Init(string jsonString)
    {
        json = jsonString;
    }

    public static List<ModelMessage> Parse()
    {
        List<ModelMessage> messagers = new();
        JObject resultReq = JObject.Parse(json);
        JToken result = resultReq["result"]!;

        foreach (JToken message in result)
        {
            ModelMessage mm = new();
            mm.FirstName = message["message"]!["from"]!["first_name"]!.ToString();
            mm.MessageText = message["message"]!["text"]!.ToString();
            mm.UpdateId = message["update_id"]!.ToString();
            mm.MessageId = message["message"]!["message_id"]!.ToString();
            mm.UserId = message["message"]!["from"]!["id"]!.ToString();
            messagers.Add(mm);
        }
        

        return messagers;
    }
     public static DateTime UnixTimeStampToDateTime( double unixTimeStamp )
{
    // Unix timestamp is seconds past epoch
    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    dateTime = dateTime.AddSeconds( unixTimeStamp ).ToLocalTime();
    return dateTime;
}
  public static ModelWeather Parse_W()
  {
    // ModelWeather mm;
   
    ModelWeather mm = new();
    JObject resultReq = JObject.Parse(json);
    Console.WriteLine(resultReq);
   
      if (resultReq!= null)
      {
      
        mm.Date = UnixTimeStampToDateTime((double)resultReq["dt"]!).ToString();
        mm.Place = resultReq["name"]!.ToString();
        mm.TemperatureF = (int) resultReq["main"]!["temp"]!;
        mm.Wind = (int) resultReq["wind"]!["speed"]!;
        mm.Summary = resultReq["weather"][0]!["description"]!.ToString();
  

      }
        return mm;
  }
        
    
}
