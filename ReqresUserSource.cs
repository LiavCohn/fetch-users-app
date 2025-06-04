using System.Text.Json;

public class ReqresUserSource : IUserSource
{
    private const string ApiKey = "reqres-free-v1";
    private const string baseUrl = "https://reqres.in/api/users";
    private const string sourceId = "Reqres";

    public async Task<List<User>> GetUsersAsync()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("x-api-key", ApiKey);
        
        var allUsers = new List<User>();
        int page = 1;
        int totalPages;

        //pagination
        do {
            var currentUrl = $"{baseUrl}?page={page}";
            var responseJson = await client.GetStringAsync(currentUrl);
            var responseDoc = JsonDocument.Parse(responseJson);
            var responseRoot = responseDoc.RootElement;
            
            totalPages = responseRoot.GetProperty("total_pages").GetInt32();
            var data = responseRoot.GetProperty("data");
 
            //break early if not data found
            if (data.GetArrayLength() < 1) {
                break;
            }
            allUsers.AddRange(data.EnumerateArray().Select(e => new User
            {
                FirstName = e.GetProperty("first_name").GetString(),
                LastName = e.GetProperty("last_name").GetString(),
                Email = e.GetProperty("email").GetString(),
                SourceId = sourceId
            }));
            page++; 
        } while (page <= totalPages);

        return allUsers;
    }
}

