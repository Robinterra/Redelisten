public interface IUserRepo
{
    User Create(CreateUserDto createUserDto);
    User? Retrieve(int id);
    void LifetimeDelete();
}