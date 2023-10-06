public interface IRedelisteRepo
{
    Redeliste? Create(CreateRedelisteDto createRedelisteDto, User user);
    Redeliste? Retrieve(string name);
    Redeliste? Delete(string name);
    List<Redeliste> Delete(List<User> moderator);
}