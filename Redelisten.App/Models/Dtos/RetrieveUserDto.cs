public class RetrieveUserDto
{
    public string Name { get; set; } = "";

    public RetrieveUserDto(User user)
    {
        Name = user.Name;
    }
}