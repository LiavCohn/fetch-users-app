using System.Text.Json;

public class RandomUserSource : IUserSource
{
    const string url = "https://randomuser.me/api/?results=500";
    const string sourceId = "RandomUserApi";

    public async Task<List<User>> GetUsersAsync()
    {
        using var client = new HttpClient();
        var json = await client.GetStringAsync(url);
        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement.GetProperty("results");

        return root.EnumerateArray().Select(e => new User
        {
            FirstName = e.GetProperty("name").GetProperty("first").GetString(),
            LastName = e.GetProperty("name").GetProperty("last").GetString(),
            Email = e.GetProperty("email").GetString(),
            SourceId = sourceId
        }).ToList();
    }
}
