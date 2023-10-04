public class RetrieveUserDto
{
    public string Name { get; set; } = "";
    public int Id { get; set; }

    public RetrieveUserDto(User user)
    {
        Name = user.Name;
        Id = user.Id;
    }
}