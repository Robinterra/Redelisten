
using System.Collections;

public class RedelisteRepo : IRedelisteRepo
{

    public Dictionary<String, Redeliste> Redelisten { get; set; } = new Dictionary<String, Redeliste>();

    public RedelisteRepo()
    {
        Redeliste redeliste = new Redeliste(new CreateRedelisteDto() { Name = "Test" }, new User(new CreateUserDto() { Name = "Admin" }, 0));

        Redelisten.Add(redeliste.Name, redeliste);
    }


    public Redeliste? Create(CreateRedelisteDto createRedelisteDto, User user)
    {
        Redeliste redeliste = new Redeliste(createRedelisteDto, user);
        if (Redelisten.ContainsKey(redeliste.Name))
            return null;
        
        Redelisten.Add(redeliste.Name, redeliste);

        return redeliste;
    }

    public Redeliste? Retrieve(string name)
    {
        return Redelisten.TryGetValue(name, out var value) ? value : null;
    }

    public Redeliste? Delete(string name)
    {
        Redeliste? deletedRedeliste = Retrieve(name);
        Redelisten.Remove(name);
        return deletedRedeliste;
    }
    
    public List<Redeliste> Delete(List<User> moderator)
    {
        List<Redeliste> deletedRedelisten = new List<Redeliste>();
        IEnumerable toDelete = Redelisten.Where(t => moderator.Contains(t.Value.Moderator));
        foreach (KeyValuePair<string, Redeliste> redeliste in toDelete)
        {
            deletedRedelisten.Add(redeliste.Value);
            Redelisten.Remove(redeliste.Key);
        }
        return deletedRedelisten;
    }
}