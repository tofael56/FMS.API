using FMS.API.Data;
using FMS.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace FMS.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/PeoductAPI")]
    [ApiController]
    public class PeoductAPIController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ProductDto>> GetProduct()
        {
            return Ok(ProductStore.GetAllProducts);
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
            var data = ProductStore.GetAllProducts.FirstOrDefault(x => x.Id == id);
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
            if (ProductStore.GetAllProducts.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower()) != null)
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
            product.Id = ProductStore.GetAllProducts.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            ProductStore.GetAllProducts.Add(product);
            //return the route of inserted data get
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }
        [HttpDelete("{id:int}",Name="DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = ProductStore.GetAllProducts.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ProductStore.GetAllProducts.Remove(product);
            return NoContent();
        }

        [HttpPut("{id:int}",Name ="UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if(productDto == null && productDto.Id != id)
            {
                return BadRequest();
            }
            var produc = ProductStore.GetAllProducts.FirstOrDefault(x => x.Id == id);
            produc.Name = productDto.Name;
            produc.Barcode = productDto.Barcode;
            produc.Code = productDto.Code;
            return NoContent();
        }
    }
}
