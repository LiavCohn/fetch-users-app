using System.Text.Json;

public class JsonDataWriter : IDataWrite
{
    public async Task WriteAsync(string filePath, List<User> users)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(users, options);
        await File.WriteAllTextAsync(filePath, json);
    }
}
