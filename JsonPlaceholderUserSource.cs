using System.Text.Json;

public class JsonPlaceholderUserSource : UserSourceBase
{
    public override string Url => "https://jsonplaceholder.typicode.com/users";
    public override string SourceId => "JsonPlaceholder";


    public override async Task<List<User>> GetUsersAsync()
    {
        using var client = new HttpClient();
        var json = await client.GetStringAsync(Url);
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
                SourceId = SourceId
            };
        }).ToList();
    }
}

