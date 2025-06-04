using System.Text;

public class CsvDataWriter : IDataWrite
{
    public async Task WriteAsync(string filePath, List<User> users)
    {
        var lines = new List<string> { "FirstName,LastName,Email,SourceId" };
        lines.AddRange(users.Select(u => $"{u.FirstName},{u.LastName},{u.Email},{u.SourceId}"));
        await File.WriteAllLinesAsync(filePath, lines);
    }
}
