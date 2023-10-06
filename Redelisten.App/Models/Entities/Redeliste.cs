public class Redeliste
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }

    public Redeliste(CreateRedelisteDto createRedelisteDto)
    {
        this.Name = createRedelisteDto.Name;
        this.CreatedAt = DateTime.UtcNow;
    }
}