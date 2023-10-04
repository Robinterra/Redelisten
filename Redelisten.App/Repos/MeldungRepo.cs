public class MeldungRepo : IMeldungRepo
{
    private Dictionary<string, List<Meldung>> Meldungen;

    private bool Contains(Meldung meldung)
    {
        return Meldungen.ContainsKey(meldung.RedelistenName)
               && Meldungen[meldung.RedelistenName].Contains(meldung);
    }
    
    public Meldung? Create(CreateMeldungDto createMeldungDto)
    {
        Meldung meldung = new Meldung(createMeldungDto);
        if (!Contains(meldung))
            return null;
        
        Meldungen[meldung.RedelistenName].Add(meldung);

        return meldung;
    }

    public List<Meldung> Retrieve(string redelistenName)
    {
        if (Meldungen.TryGetValue(redelistenName, out var value)) return new List<Meldung>(value);

        return new List<Meldung>();
    }

    public bool Delete(Meldung meldung)
    {
        return Meldungen.TryGetValue(meldung.RedelistenName, out var value)
               && value.Remove(meldung);
    }
}