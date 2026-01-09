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

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <ProductDto> GetProduct(int id)
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
    }
}
