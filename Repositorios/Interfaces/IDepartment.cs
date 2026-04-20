using webAPI_ASPNET.Models;

namespace webAPI_ASPNET.Repositorios.Interfaces
{
    public interface IDepartment
    {
        Task<List<Department>> getAll();
        Task<Department> getById(int id);
        Task<Department> post(Department department);
        Task<Department> put(Department department, int id);
        Task<bool> delete(int id);
    }

    public interface IDepartmentRelation
    {

        Task<List<DepartmentRelation>> getAll();
        Task<DepartmentRelation> getById(int id);
        Task<DepartmentRelation> post(DepartmentRelation department);
        Task<DepartmentRelation> put(DepartmentRelation department, int id);
        Task<bool> delete(int id);
    }
}
