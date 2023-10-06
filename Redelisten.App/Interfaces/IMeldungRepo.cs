public interface IMeldungRepo
{
    Meldung? Create(CreateMeldungDto createMeldungDto, User user);
    List<Meldung> Retrieve(string RedelistenName);
    bool Delete(List<User> users);
    bool Delete(Meldung meldung);
    bool DeleteFromRedeliste(string nameRedeliste);
}