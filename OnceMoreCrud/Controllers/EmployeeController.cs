using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnceMoreCrud.Model;

namespace OnceMoreCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeDbContext _Db;

        public EmployeeController(EmployeeDbContext db)
        {
            _Db = db;
        }

        [HttpGet]
        public IActionResult TOList() { 
        
           var list = _Db.Employees.ToList();
            return Ok(list);
        }
        [HttpGet("{id}")]
       // [Route("id")]
        public IActionResult GetById(int id) { 
          var GetById = _Db.Employees.Find(id);
            return Ok(GetById);
        }

        [HttpPost]
        public IActionResult Create(Employee employee) { 
        
            var create = _Db.Employees.Add(employee);

            _Db.SaveChanges();

            return Created("GetById",employee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,Employee employee) 
            {
            var update = _Db.Employees.Find(id);
            update.name = employee.name;
            update.salary = employee.salary;
            _Db.SaveChanges();
            return Ok(update);
            
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var delete = _Db.Employees.Find(id);
            _Db.Employees.Remove(delete);
            _Db.SaveChanges();
            return Ok(delete);
        }

    }
}
