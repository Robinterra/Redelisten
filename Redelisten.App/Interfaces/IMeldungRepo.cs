public interface IMeldungRepo
{
    Meldung? Create(CreateMeldungDto createMeldungDto);
    List<Meldung> Retrieve(string RedelistenName);
    bool Delete(Meldung meldung);
}