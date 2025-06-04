//Helper interface to get users from a source
public interface IUserSource
{
    Task<List<User>> GetUsersAsync();
}