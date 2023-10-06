public interface IRedelisteRepo
{
    Redeliste? Create(CreateRedelisteDto createRedelisteDto);
    Redeliste? Retrieve(string name);
    bool Delete(string name);
}