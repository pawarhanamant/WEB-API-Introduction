using CRUDEFcore.Model;
using CRUDEFcore.Model.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CRUDEFcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CtegoriesController : ControllerBase
    {
        private readonly CtegoriesContext _context;

        public CtegoriesController(CtegoriesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult TOList() {

            var ctegories = _context.Ctegories.ToList();

            return Ok(ctegories);
        }
        [HttpGet]
        [Route("Id")]
        public IActionResult GetbyID(int Id)
        {
            if(Id <= 0)
                return BadRequest();
            var getbyid = _context.Ctegories.Find(Id);
            return Ok(getbyid);
        }
        [HttpPost]
        public IActionResult Create( Ctegories ctegories) {

            if (ModelState.IsValid)
            {
                var Create = _context.Ctegories.Add(ctegories);
                _context.SaveChanges();

                return Created("GetbyId",ctegories);

            }
            else { 
              return BadRequest();
            }
        }

        [HttpPut]

        public IActionResult Update(int id ,Ctegories ctegories) {

            if (id > 0) {

                if (id == ctegories.Id) {

                    var ctegori = _context.Ctegories.Find(id);

                    if (ctegori != null)
                    {

                        ctegori.Name = ctegories.Name;
                        ctegori.Description = ctegories.Description;
                        _context.SaveChanges();

                        return Ok();
                    }
                    return NotFound();
                }
                return BadRequest();
            }

            return BadRequest();
        }


        [HttpDelete]
        public IActionResult Delete(int id) {

            if (id >0) { 

                var ctegor = _context.Ctegories.Find(id);
                if (ctegor != null) { 

                    _context.Ctegories.Remove(ctegor);
                    _context.SaveChanges();

                    return Ok();
                
                }
                return NotFound();

            
            }
            return BadRequest();
        }
    }
}
