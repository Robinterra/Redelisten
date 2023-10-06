public class RetrieveRedelisteDto
{
    public string Name { get; set; }
    public Redeliste.PriorityAlgorithm Algorithm { get; set; }
    public List<RetrieveUserDto> Meldungen { get; set; }
    public int ModeratorId { get; set; }

    public RetrieveRedelisteDto(Redeliste redeliste, List<User> users)
    {
        Name = redeliste.Name;
        Algorithm = redeliste.Algorithm;
        ModeratorId = redeliste.Moderator.Id;
        Meldungen = users.ConvertAll(user => new RetrieveUserDto(user));
    }
}