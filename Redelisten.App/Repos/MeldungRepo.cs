using System.Collections;

public class MeldungRepo : IMeldungRepo
{
    private Dictionary<string, List<Meldung>> Meldungen = new Dictionary<string, List<Meldung>>();

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
    public bool Delete(List<User> users)
    {
        bool result = false;
        foreach (KeyValuePair<string, List<Meldung>> keyValuePair in Meldungen)
        {
            foreach (Meldung meldung in keyValuePair.Value)
            {
                 if (users.Any(t => t.Id == meldung.UserID))
                 {
                     result = keyValuePair.Value.Remove(meldung);
                 }
            }
        }
        return result;
    }
    public bool Delete(Meldung meldung)
    {
        return Meldungen.TryGetValue(meldung.RedelistenName, out var value)
               && value.Remove(meldung);
    }

    public bool DeleteFromRedeliste(string nameRedeliste)
    {
        return Meldungen.Remove(nameRedeliste);
    }
    
}