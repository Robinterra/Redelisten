public class UserRepo : IUserRepo
{
    private int counter = 0;
    public Dictionary<int, User> Users { get; set; } = new Dictionary<int, User>();

    public UserRepo()
    {

    }

    public User Create(CreateUserDto createUserDto)
    {
        User user = new User(createUserDto, counter++);

        this.Users.Add(user.Id, user);

        return user;
    }

    public User? Retrieve(int id)
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
        //TODO: Meldungen und Räume der Nutzer löschen
        foreach (User user in toDelete)
        {
            Users.Remove(user.Id);
        }
    }

}