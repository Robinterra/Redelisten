public class UserRepo : IUserRepo
{

    public Dictionary<Guid, User> Users { get; set; } = new Dictionary<Guid, User>();

    public UserRepo()
    {

    }

    public User Create(CreateUserDto createUserDto)
    {
        User user = new User(createUserDto);

        this.Users.Add(user.Id, user);

        return user;
    }

    public User? Retrieve(Guid id)
    {
        return Users.TryGetValue(id, out var value) ? value: null;
    }

    public void LifetimeDelete()
    {
        List<User> toDelete = new List<User>();
        foreach (User user in Users.Values)
        {
            if(user.CreatedAt.AddDays(1) < DateTime.UtcNow)
            {
                toDelete.Add(user);
            }
        }
        
        foreach (User user in toDelete)
        {
            Users.Remove(user.Id);
        }
    }

}