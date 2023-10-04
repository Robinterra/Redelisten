public class RetrieveRedelisteDto
{
    public string Name { get; set; }
    public List<RetrieveUserDto> Meldungen { get; set; }

    public RetrieveRedelisteDto(Redeliste redeliste, List<User> users)
    {
        Name = redeliste.Name;
        Meldungen = users.ConvertAll(user => new RetrieveUserDto(user));
    }
}