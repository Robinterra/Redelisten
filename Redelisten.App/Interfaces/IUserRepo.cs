public interface IUserRepo
{
    User Create(CreateUserDto createUserDto);
    User? Retrieve(Guid UserID);
}