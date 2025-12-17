using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTasksController : ControllerBase
    {
        private readonly ApiContext _context;
        public EmployeeTasksController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult EmployeeTasksList()
        {
            var values = _context.EmployeeTasks.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateEmployeeTasks(EmployeeTask EmployeeTasks)
        {
            _context.EmployeeTasks.Add(EmployeeTasks);
            _context.SaveChanges();
            return Ok("Ekleme işlemi başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteEmployeeTasks(int id)
        {
            var value = _context.EmployeeTasks.Find(id);
            _context.EmployeeTasks.Remove(value);
            _context.SaveChanges();
            return Ok("silme işlemi başarılı");
        }

        [HttpGet("GetEmployeeTasks")]
        public IActionResult GetEmployeeTasks(int id)
        {
            var value = _context.EmployeeTasks.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateEmployeeTasks(EmployeeTask EmployeeTasks)
        {
            _context.EmployeeTasks.Update(EmployeeTasks);
            _context.SaveChanges();
            return Ok("güncelleme işlemi başarılı");
        }
    }
}
