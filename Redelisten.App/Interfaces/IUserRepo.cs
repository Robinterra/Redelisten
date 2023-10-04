public interface IUserRepo
{
    User Create(CreateUserDto createUserDto);
    User? Retrieve(Guid id);
    void LifetimeDelete();
}