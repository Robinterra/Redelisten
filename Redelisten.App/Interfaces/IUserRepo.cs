public interface IUserRepo
{
    User Create(CreateUserDto createUserDto);
    void LifetimeDelete();
}