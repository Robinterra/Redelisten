public class CreateMeldungDto
{
    public User Moderator { get; }
    public string RedelistenName { get; }

    public CreateMeldungDto(User moderator, string redelistenName)
    {
        Moderator = moderator;
        RedelistenName = redelistenName;
    }
    
}