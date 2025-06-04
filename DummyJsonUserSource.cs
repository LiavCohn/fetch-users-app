using System.Text.Json;

public class DummyJsonUserSource : IUserSource
{
    const string url = "https://dummyjson.com/users";
    const string sourceId = "DummyJson";
    
    public async Task<List<User>> GetUsersAsync()
    {
        using var client = new HttpClient();
        var json = await client.GetStringAsync(url);
        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement.GetProperty("users");

        return root.EnumerateArray().Select(e => new User
        {
            FirstName = e.GetProperty("firstName").GetString(),
            LastName = e.GetProperty("lastName").GetString(),
            Email = e.GetProperty("email").GetString(),
            SourceId = sourceId
        }).ToList();
    }
}

