public interface IUserRepo
{
    User Create(CreateUserDto createUserDto);
    User? Retrieve(int id);
    User? Retrieve(Guid id);
    void LifetimeDelete();
}