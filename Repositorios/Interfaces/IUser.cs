using webAPI_ASPNET.Models;

namespace webAPI_ASPNET.Repositorios.Interfaces
{
    public interface IUser
    {
        Task<List<UserWithDepartment>> getAll();
        Task<User> getById(int id);
        Task<UserWithDepartment> getUserIDepartmentById(int id);
        Task<UserWithDepartment> getUserIDepartmentByUsernameAndPassword(string username, string password);
        Task<UserWithButton> getUserIButtonById(int id);
        Task<User> post(User user);
        Task<User> put(User user, int id);
        Task<bool> delete(int id);
        User Authenticate(string username, string password);
        Task VincularDepartamento(int idUser, int idDepartamento);
    }
}
