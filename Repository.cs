using Newtonsoft.Json;
public struct Repository
{
    static Dictionary<string, List<ModelMessage>> db = new();

    public static void Append(ModelMessage model)
    {
        var id = model.UserId;
        if (db.ContainsKey(id))
        {
            db[id].Add(model);
        }
        else
        {
            db.Add(id, new List<ModelMessage>(new ModelMessage[] { model }));
        }
    }

    public static Dictionary<string, List<ModelMessage>> Read()
    {
        return db;
    }

    public static string GetString()
    {
        string data = String.Empty;


        foreach (var item in db)
        {
            data += $"{item.Key} [{String.Join(',', item.Value)}]\n";
        }
        return $"{data}\n\n";

    }

    public static string Save()
    {
        var js = JsonConvert.SerializeObject(db);
        File.WriteAllText("data.json", js);
        return js;
    }

    public static void Load()
    {
        db = JsonConvert.DeserializeObject<Dictionary<string, List<ModelMessage>>>(File.ReadAllText("data.json"))!;
    }
}
