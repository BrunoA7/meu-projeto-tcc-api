using Microsoft.EntityFrameworkCore;
using webAPI_ASPNET.Data;
using webAPI_ASPNET.Models;
using webAPI_ASPNET.Repositorios.Interfaces;

namespace webAPI_ASPNET.Repositorios
{
    public class DepartmentRepositorio : IDepartment
    {
        private readonly AppDbContext _dbContext;
        public DepartmentRepositorio(AppDbContext AppDbContext)
        {
            _dbContext = AppDbContext;
        }

        public async Task<List<Department>> getAll()
        {
            return await _dbContext.Department.ToListAsync();
        }

        public async Task<Department> getById(int id)
        {
            return await _dbContext.Department.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Department> post(Department Department)
        {
            await _dbContext.AddAsync(Department);
            await _dbContext.SaveChangesAsync();

            return Department;
        }

        public async Task<Department> put(Department department, int id)
        {
            Department departmentById = await getById(id);
            if (departmentById == null)
            {
                throw new Exception("Department not found");
            }

            departmentById.DEPARTMENTNAME = department.DEPARTMENTNAME;
            departmentById.DESCRIPTION = department.DESCRIPTION;

            _dbContext.Update(departmentById);
            await _dbContext.SaveChangesAsync();
            return department;
        }

        public async Task<bool> delete(int id)
        {
            Department departmentById = await getById(id);
            if(departmentById == null)
            {
                throw new Exception("Department not found");
            }

            _dbContext.Remove(departmentById);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

    public class DepartmentRelationRepositorio : IDepartmentRelation
    {
        private readonly AppDbContext _dbContext;
        public DepartmentRelationRepositorio(AppDbContext AppDbContext)
        {
            _dbContext = AppDbContext;
        }


        public async Task<List<DepartmentRelation>> getAll()
        {
            return await _dbContext.DepartmentRelation.ToListAsync();
        }

        public async Task<DepartmentRelation> getById(int id)
        {
            return await _dbContext.DepartmentRelation.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<DepartmentRelation> post(DepartmentRelation department)
        {
            await _dbContext.AddAsync(department);
            await _dbContext.SaveChangesAsync();

            return (department);
        }

        public async Task<DepartmentRelation> put(DepartmentRelation departmentRelation, int id)
        {
            DepartmentRelation departmentRelationById = await getById(id);
            if (departmentRelationById == null)
            {
                throw new Exception("Relation not found");
            }

            departmentRelationById.IDDEPARTMENT = departmentRelation.IDDEPARTMENT;
            departmentRelationById.IDUSER = departmentRelation.IDUSER;

            _dbContext.Update(departmentRelationById);
            await _dbContext.SaveChangesAsync();
            return (departmentRelation);
            
        }

        public async Task<bool> delete(int id)
        {
            DepartmentRelation departmentRelationById = await getById(id);
            if (departmentRelationById == null)
            {
                throw new Exception("Relation not found");
            }

            _dbContext.Remove(departmentRelationById);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
