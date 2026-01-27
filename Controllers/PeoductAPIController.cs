using FMS.API.Data;
using FMS.API.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FMS.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/PeoductAPI")]
    [ApiController]
    public class PeoductAPIController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PeoductAPIController> _logger;
        public  PeoductAPIController(ILogger<PeoductAPIController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetProduct()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var data = _db.Products.FirstOrDefault(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> CreateProduct([FromBody] ProductDto product)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //custome model stte validation
            if (_db.Products.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Product Already Exists!");
                return BadRequest(ModelState);
            }
            if (product == null)
            {
                return BadRequest(product);
            }
            if (product.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            ProductModel model = new()
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Barcode = product.Barcode,

            };
            _db.Products.Add(model);
            _db.SaveChanges();
            //return the route of inserted data get
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = _db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null && productDto.Id != id)
            {
                return BadRequest();
            }
            ProductModel model = new()
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Code = productDto.Code,
                Barcode = productDto.Barcode,
            };
            _db.Products.Update(model);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id:int}",Name ="UpdatePartialproduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialProduct(int id,JsonPatchDocument<ProductDto> patchDto)
        {
            if(patchDto==null && id == 0)
            {
                return BadRequest();
            }
            var produc = _db.Products.FirstOrDefault(x => x.Id == id);
            ProductDto modelDto = new()
            {
                Id = produc.Id,
                Name = produc.Name,
                Code = produc.Code,
                Barcode = produc.Barcode,
            };

            if (produc == null)
            {
                return BadRequest();
            }
            patchDto.ApplyTo(modelDto, ModelState);
            ProductModel productModel = new()
            {
                Id = modelDto.Id,
                Name = modelDto.Name,
                Code = modelDto.Code,
                Barcode = modelDto.Barcode,
            };
            _db.Products.Update(productModel);
            _db.SaveChanges();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
