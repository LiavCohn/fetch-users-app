public abstract class UserSourceBase
{
    public abstract string Url { get; }
    public abstract string SourceId { get; }

    public abstract Task<List<User>> GetUsersAsync();
}
