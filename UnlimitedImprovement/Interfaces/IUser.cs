using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface IUser
    {
        public List<User> GetAllUsers();
        public User? GetUserById(int id);
        public User CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(int id);
    }
}
