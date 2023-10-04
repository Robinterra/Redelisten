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

}