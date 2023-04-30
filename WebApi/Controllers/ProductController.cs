using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_DAL;
using WebApi_DAL.Models;
using WebApi_DAL.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductServices _productService;
        private readonly ProductContext _productContext;


        public ProductController(IProductServices productService, ProductContext productContext)
        {
            _productService = productService;
            _productContext = productContext;
        }
        [HttpGet(nameof(GetProducts))]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAll();
            if (_productService == null)
            {
                return NotFound();

            }
            return Ok(products);
        }
        [HttpGet("{id}")]

        public Task<ActionResult<Product>> GetProductId(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return Task.FromResult<ActionResult<Product>>(NotFound());
            }
            return Task.FromResult<ActionResult<Product>>(Ok(product));

        }
        [HttpPost(nameof(AddProduct))]
        public IActionResult AddProduct(Product product)
        {
            if (product != null)
            {
                _productService.Add(product);
                return Ok("create succesfully");
            }
            else
            {
                return BadRequest();
            }


        }

        //public IActionResult Update([FromBody] Product product, [FromRoute] int id)
        // {
        //   _productService.Update(id,product);
        //   return Ok();
        // }


        [HttpPost]
       public async Task <ActionResult<Product>> Update(  Product product )
        {
             _productContext.Products.Add(product);
           await _productContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductId), new {id= product.Id}, product);
                      
       }
        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateProduct(int id, Product product) 
        {
            if (id!=product.Id)
            {
                return BadRequest();
            }
            _productContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _productContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw;
            }
            return Ok();
        }


        private bool ProductExists(int id)
        {
            return (_productContext.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }





    }




 
    

   


       
 
