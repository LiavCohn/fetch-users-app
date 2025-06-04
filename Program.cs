using System;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Please enter output file path:");
        string filePath = Console.ReadLine();

        Console.WriteLine("Choose a format (csv, json):");
        string format = Console.ReadLine().ToLower();
        IDataWrite dataWriter = format == "csv" ? new CsvDataWriter() : new JsonDataWriter();

        //construct the full path
        string fileName = $"users_{DateTime.Now:yyyyMMddHHmmss}.{format}";
        string fullPath = Path.Combine(filePath, fileName);

        Console.WriteLine($"Writing to {filePath} in {format} format...");

        //list of user sources
        List<IUserSource> userSources = new List<IUserSource>
        {
            new RandomUserSource(),
            new JsonPlaceholderUserSource(),
            new DummyJsonUserSource(),
            new ReqresUserSource()  
        };
        
        List<User> users = new List<User>();

        var tasks = userSources.Select(s => s.GetUsersAsync());
        var results = await Task.WhenAll(tasks);
        users.AddRange(results.SelectMany(r => r));

        await dataWriter.WriteAsync(fullPath, users);
        Console.WriteLine("Done! Saved to " + fullPath + " " + users.Count + " users");
    }
}
