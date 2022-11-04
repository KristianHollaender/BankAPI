using Domain;

namespace Application.Interfaces;

public interface IUserRepository
{
    public User CreateNewUSer(User user);
    public User GetUserByEmail(string email);
}