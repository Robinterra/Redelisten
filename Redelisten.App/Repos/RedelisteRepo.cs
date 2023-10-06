public class RedelisteRepo : IRedelisteRepo
{

    public Dictionary<String, Redeliste> Redelisten { get; set; } = new Dictionary<String, Redeliste>();


    public Redeliste? Create(CreateRedelisteDto createRedelisteDto)
    {
        Redeliste redeliste = new Redeliste(createRedelisteDto);
        if (Redelisten.ContainsKey(redeliste.Name))
            return null;
        
        Redelisten.Add(redeliste.Name, redeliste);

        return redeliste;
    }

    public Redeliste? Retrieve(string name)
    {
        return Redelisten.TryGetValue(name, out var value) ? value : null;
    }

    public bool Delete(string name)
    {
        return Redelisten.Remove(name);
    }
}