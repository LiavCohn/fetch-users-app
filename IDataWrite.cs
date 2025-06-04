//Helper interface to write data to a file -> csv or json
public interface IDataWrite
{
    Task WriteAsync(string filePath, List<User> users);
}
