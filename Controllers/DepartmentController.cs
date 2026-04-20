using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webAPI_ASPNET.Models;
using webAPI_ASPNET.Repositorios;
using webAPI_ASPNET.Repositorios.Interfaces;

namespace webAPI_ASPNET.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _departmentRepositorio;

        public DepartmentController(IDepartment departmentRepositorio)
        {
            _departmentRepositorio = departmentRepositorio;
        }

        [Authorize("perm")]
        [HttpGet]
        public async Task<ActionResult<List<Department>>> getAll()
        {
            List<Department> department = await _departmentRepositorio.getAll();
            return Ok(department);
        }

        [Authorize("perm")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> getById(int id)
        {
            Department department = await _departmentRepositorio.getById(id);
            return Ok(department);
        }

        [Authorize("perm")]
        [HttpPost]
        public async Task<ActionResult<Department>> post([FromBody] Department departmentModel)
        {
            Department department = await _departmentRepositorio.post(departmentModel);
            return Ok(department);
        }

        [Authorize("perm")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> put([FromBody] Department departmentModel, int id)
        {
            Department department = await _departmentRepositorio.put(departmentModel, id);
            return Ok(department);
        }

        [Authorize("perm")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> delete(int id)
        {
            bool apagado = await _departmentRepositorio.delete(id);
            return Ok(apagado);
        }
    }

    [Route("[controller]")]
    [ApiController]
    public class DepartmentRelationController : ControllerBase
    {
        private readonly IDepartmentRelation _departmentRelationRepositorio;

        public DepartmentRelationController(IDepartmentRelation departmentRelationRepositorio)
        {
            _departmentRelationRepositorio = departmentRelationRepositorio;
        }

        [Authorize("perm")]
        [HttpGet]
        public async Task<ActionResult<List<DepartmentRelation>>> getAll()
        {
            List<DepartmentRelation> departmentRelation = await _departmentRelationRepositorio.getAll();
            return Ok(departmentRelation);
        }

        [Authorize("perm")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentRelation>> getById(int id)
        {
            DepartmentRelation departmentRelation = await _departmentRelationRepositorio.getById(id);
            return Ok(departmentRelation);
        }

        [Authorize("perm")]
        [HttpPost]
        public async Task<ActionResult<DepartmentRelation>> post([FromBody] DepartmentRelation departmentRelationModel)
        {
            DepartmentRelation departmentRelation = await _departmentRelationRepositorio.post(departmentRelationModel);
            return Ok(departmentRelation);
        }

        [Authorize("perm")]
        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentRelation>> put([FromBody] DepartmentRelation departmentModel, int id)
        {
            DepartmentRelation department = await _departmentRelationRepositorio.put(departmentModel, id);
            return Ok(department);
        }

        [Authorize("perm")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentRelation>> delete(int id)
        {
            bool apagado = await _departmentRelationRepositorio.delete(id);
            return Ok(apagado);
        }
    }
}
