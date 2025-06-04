using System.Text.Json;

public class DummyJsonUserSource : UserSourceBase
{

    public override string Url => "https://dummyjson.com/users";
    public override string SourceId => "DummyJson";

    
    public override async Task<List<User>> GetUsersAsync()
    {
        using var client = new HttpClient();
        var json = await client.GetStringAsync(Url);
        var doc = JsonDocument.Parse(json);
        var root = doc.RootElement.GetProperty("users");

        return root.EnumerateArray().Select(e => new User
        {
            FirstName = e.GetProperty("firstName").GetString(),
            LastName = e.GetProperty("lastName").GetString(),
            Email = e.GetProperty("email").GetString(),
            SourceId = SourceId
        }).ToList();
    }
}

