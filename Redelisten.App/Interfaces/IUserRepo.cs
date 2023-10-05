public interface IUserRepo
{
    User? Create(CreateUserDto createUserDto);
    User? Retrieve(int id);
    User? Retrieve(Guid id);
    List<User> UsersToDelete();
    bool Delete(List<User> toDelete);
}