using CRUDEFcore.Model;
using CRUDEFcore.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDEFcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CtegoriesContext _context;

        public ProductController(CtegoriesContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetList() { 
         
            var product = _context.productTbls.ToList();

            return Ok(product);

        }
        [HttpGet]
        [Route("Id")]
        public IActionResult GetById(int id) {

            if (id <= 0)
                return BadRequest();

            var idby = _context.productTbls.Find(id);
            return Ok(idby);
        }

        [HttpPost]
        public IActionResult Create(ProductTbl productTbl) {

            if (ModelState.IsValid)
            {
                var products = _context.productTbls.Add(productTbl);
                _context.SaveChanges();

                return Created("GetbyId", productTbl);


            }
            else {

                return BadRequest();
            }
        }


        [HttpPut]
        public IActionResult Edit( int Id,ProductTbl productTbl) {

            if (Id > 0) {

                if (Id == productTbl.Id) {

                    var id = _context.productTbls.Find(Id);
                    if (id != null) { 
                    
                        id.Name = productTbl.Name;
                        id.Description = productTbl.Description;
                        id.CtrgoriesId = productTbl.CtrgoriesId;

                        _context.SaveChanges();
                        return Ok(id);

                    }
                    return NotFound();
                
                }
                return BadRequest();
            
            
            }
            return BadRequest();
        }


        [HttpDelete]
        public IActionResult Delete(int id) {

            if (id > 0) { 
            
                var delete = _context.productTbls.Find(id);
                if (delete != null) {

                    _context.productTbls.Remove(delete);
                    _context.SaveChanges();

                    return Ok();
                
                }
                return NotFound();
            }
            return BadRequest();
        

        }

        
    }
}
