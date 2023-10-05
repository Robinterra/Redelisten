public class UserRepo : IUserRepo
{
    private int counter = 0;
    private Dictionary<int, User> Users { get; set; } = new Dictionary<int, User>();

    public UserRepo()
    {

    }

    public User? Create(CreateUserDto createUserDto)
    {
        if (this.Users.Any(t=>t.Value.Name == createUserDto.Name)) return null;

        User user = new User(createUserDto, counter++);

        this.Users.Add(user.Id, user);

        return user;
    }

    public User? Retrieve(int id)
    {
        return Users.TryGetValue(id, out var value) ? value: null;
    }

    public List<User> UsersToDelete()
    {
        List<User> toDelete = new List<User>();
        foreach (User user in Users.Values)
        {
            if(user.CreatedAt.AddDays(1) < DateTime.UtcNow)
            {
                toDelete.Add(user);
            }
        }
        return toDelete;
    }

    public User? Retrieve(Guid id)
    {
        KeyValuePair<int, User> foundUser = Users.FirstOrDefault(x => x.Value.Token == id);

        return foundUser.Value;
    }

    public bool Delete(List<User> toDelete)
    {
        bool result = false;
        foreach (User user in toDelete)
        {
             result = Users.Remove(user.Id);
        }
        return result;
    }
}