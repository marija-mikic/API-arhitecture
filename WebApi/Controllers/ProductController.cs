using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi_DAL;
using WebApi_DAL.Models;
using WebApi_DAL.Repository;
using WebApi_DAL.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly  IProductServices _productService;
        private readonly ProductContext _productContext;


        public ProductController(IProductServices productService, ProductContext productContext)
        {
            _productService = productService;
            _productContext = productContext;
        }
        [HttpGet(nameof(GetProducts))]
        public async Task<IActionResult> GetProducts()
        {
            var product = await _productService.GetAll();
            if (_productService==null)
            {
                return NotFound();

            }
            return Ok(product);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        
    }
    }

}
   


       
 
