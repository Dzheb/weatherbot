
using System.Diagnostics;

public struct Bot
{
    static string? token;
    static string? token_w;
    static string? baseUri;
    static string? weatherUri;

    static HttpClient hc = new HttpClient();

    public static async void Start()
    {
        int offset = 0;
        while (true)
        {
            string url = $"{baseUri}getUpdates?offset={offset}";
            string json = hc.GetStringAsync(url).Result;

            JsonParse.Init(json);
            List<ModelMessage> msgs = JsonParse.Parse();

            foreach (ModelMessage msg in msgs)            
            {
                System.Console.WriteLine(msg);
                Repository.Append(msg);
                string uid = msg.UserId;
                string msgText = String.Empty;
            Console.WriteLine("Стартовали");

                if (!Game.db.ContainsKey(uid) || msg.MessageText == "/help")
                { 
                    if (!Game.db.ContainsKey(uid)) Game.db.Add(uid, "");
                    msgText = $"Привет! Для получения погоды в городе наберите название города, для выхода /stop, для помощи /help\n";
                    
                    // SendMessage(uid, msgText, msg.MessageId);
                }
                 
                else
                {
                    string city = msg.MessageText;
                    msgText += $"Введите город:\n";
                   
                    string url_w = $"{weatherUri}{city}&APPID={token_w}";
                    HttpResponseMessage t = await new HttpClient().GetAsync(url_w);
                    if (t.IsSuccessStatusCode)
                    {

                    string res = new HttpClient().GetStringAsync(url_w).Result;
                    Console.WriteLine(t.StatusCode);
                    JsonParse.Init(res);
                    ModelWeather item = JsonParse.Parse_W();
                    Console.WriteLine($"Время: {item.Date} город: {item.Place} погода: {item.Summary} температура: {item.TemperatureC} C скорость ветра: {item.Wind} м/с");

                        if (!Game.db.ContainsKey(uid)) 
                        Game.db.Add(uid, $"Время: {item.Date} город: {item.Place} погода: {item.Summary} температура: {item.TemperatureC} C скорость ветра: {item.Wind} м/с");
                        msgText += $"Погода в городе {city}: {Game.db[uid]}.\n Перезапуск /restart";

                        }
                        else
                        {
                        Console.WriteLine("Что-то пошло не так...");
                        msgText += $"Что-то пошло не так...возможно город введён неверно\n";
                        Console.WriteLine(t.StatusCode);
                        }

             if (msg.MessageText == "/stop")
                {
                    break;
                }     
                SendMessage(uid, msgText, msg.MessageId);
                offset = (Int32.Parse(msg.UpdateId) + 1);
                // if (msg.Id == "") { BotCommandHi()}
                // if (msg.Id == "") { }
            }
            Repository.Save();
                Thread.Sleep(2000);

        }
    }
    }

    public static void Init(string publicToken, string weatherToken)
    {
        token = publicToken;
        token_w = weatherToken;
        baseUri = $"https://api.telegram.org/bot{token}/";
        weatherUri = $"https://api.openweathermap.org/data/2.5/weather?q=";//$"https://api.openweathermap.org/data/2.5/weather?q=Velsk&APPID={token}"
    }

    public static void SendMessage(string id, string text, string replyToMessageId = "")
    {
        string url = $"{baseUri}sendmessage?chat_id={id}&text={text}&reply_to_message_id={replyToMessageId}";
        var req = hc.GetStringAsync(url).Result;
    }
}
