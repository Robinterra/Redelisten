using System.Collections;

public class MeldungRepo : IMeldungRepo
{
    private Dictionary<string, List<Meldung>> Meldungen = new Dictionary<string, List<Meldung>>();

    private bool Contains(Meldung meldung)
    {
        return Meldungen.ContainsKey(meldung.RedelistenName)
               && Meldungen[meldung.RedelistenName].Contains(meldung);
    }

    private List<Meldung> NewOrdering(List<Meldung> meldungen)
    {
        List<Meldung> result = meldungen.OrderByDescending(meldung => meldung.Order).ToList();

        for (int i = 0; i < result.Count; i++)
        {
            result[i].Order = i;
        }

        return result;
    }

    public Meldung? Create(CreateMeldungDto createMeldungDto)
    {
        List<Meldung> meldungen = this.Meldungen[createMeldungDto.RedelistenName];
        meldungen = NewOrdering(meldungen);

        int maxOrder = meldungen.Max(meldung => meldung.Order);
        int newOrder = maxOrder + 1;

        Meldung meldung = new Meldung(createMeldungDto, newOrder);
        if (!Contains(meldung)) return null;

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