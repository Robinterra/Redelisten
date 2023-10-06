public class Redeliste
{
    public enum PriorityAlgorithm
    {
        Queue = 0,
        Balanced = 1
    }
    public string Name { get; set; }
    public User Moderator { get; set; }
    public DateTime CreatedAt { get; set; }
    public PriorityAlgorithm Algorithm { get; set; } 

    public Redeliste(CreateRedelisteDto createRedelisteDto, User user)
    {
        this.Name = createRedelisteDto.Name;
        this.Algorithm = createRedelisteDto.Algorithm;
        this.CreatedAt = DateTime.UtcNow;
        this.Moderator = user;
    }
}