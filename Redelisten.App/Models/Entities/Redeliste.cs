public class Redeliste
{
    public string Name { get; set; }
    public User Moderator { get; set; }
    public DateTime CreatedAt { get; set; }

    public Redeliste(CreateRedelisteDto createRedelisteDto)
    {
        if (createRedelisteDto.Moderator is null) throw new ArgumentNullException(nameof(createRedelisteDto.Moderator));

        this.Name = createRedelisteDto.Name;
        this.CreatedAt = DateTime.UtcNow;
        this.Moderator = createRedelisteDto.Moderator;
    }
}