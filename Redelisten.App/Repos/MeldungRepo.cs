using System.Collections;

public class MeldungRepo : IMeldungRepo
{
    private Dictionary<string, List<Meldung>> Meldungen = new Dictionary<string, List<Meldung>>();

    public MeldungRepo()
    {
        Meldung meldung = new Meldung(new CreateMeldungDto() { RedelistenName = "Test" }, new User(new CreateUserDto() { Name = "Admin" }, 0), int.MaxValue);
        Meldung meldung2 = new Meldung(new CreateMeldungDto() { RedelistenName = "Test" }, new User(new CreateUserDto() { Name = "Robin" }, 1), int.MaxValue);

        List<Meldung> meldungen = new List<Meldung>();
        meldungen.Add(meldung);
        meldungen.Add(meldung2);
        Meldungen.Add(meldung.RedelistenName, meldungen);
    }

    private bool Contains(Meldung meldung)
    {
        if (!Meldungen.ContainsKey(meldung.RedelistenName)) return false;
        if (Meldungen[meldung.RedelistenName].Count == 0) return false;
        if (!Meldungen[meldung.RedelistenName].Any(t=>t == meldung)) return false;

        return true;
    }

    public Meldung? Create(CreateMeldungDto createMeldungDto, User user)
    {
        List<Meldung>? meldungen = null;
        if (!Meldungen.TryGetValue(createMeldungDto.RedelistenName, out meldungen))
        {
            meldungen = new List<Meldung>();
            Meldungen.Add(createMeldungDto.RedelistenName, meldungen);
        }

        Meldung meldung = new Meldung(createMeldungDto, user, int.MaxValue);
        if (this.Contains(meldung)) return null;

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