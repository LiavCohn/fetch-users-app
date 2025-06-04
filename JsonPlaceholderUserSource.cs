using System.Text.Json;

public class JsonPlaceholderUserSource : IUserSource
{
    const string url = "https://jsonplaceholder.typicode.com/users";
    const string sourceId = "JsonPlaceholder";

    public async Task<List<User>> GetUsersAsync()
    {
        using var client = new HttpClient();
        var json = await client.GetStringAsync(url);
        var doc = JsonDocument.Parse(json);

        return doc.RootElement.EnumerateArray().Select(e => 
        {
            var name = e.GetProperty("name").GetString();
            var nameParts = name.Split(' ');
            var firstName = nameParts[0];
            var lastName = nameParts.Length > 1 ? nameParts[1] : "";
            return new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = e.GetProperty("email").GetString(),
                SourceId = sourceId
            };
        }).ToList();
    }
}

