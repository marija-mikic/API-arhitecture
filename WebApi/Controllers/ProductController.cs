using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using WebApi_DAL;
using WebApi_DAL.Models;
using WebApi_DAL.Pagination;
using WebApi_DAL.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductServices _productService;
        private readonly ProductContext _productContext;
        private readonly IValidator<Product> _validator;
        
         


        public ProductController(IProductServices productService, ProductContext productContext, IValidator<Product> validator)
        { 
            _productService = productService; 
            _productContext = productContext;
            _validator = validator;
            
             
            
        }
        [HttpGet(nameof(GetProducts))]
        public async Task<IActionResult> GetProducts([FromQuery]Paging paging)
        {
            
            var products = await _productService.GetAll(paging);
            if (_productService == null)
            {
                return NotFound();

            }
            var productPage  = JsonConvert.SerializeObject(products);
             

            return Ok(productPage);
        }
        

         
            [HttpGet(nameof(GetById))]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<Product>> GetById(int id)
            {
                var hotel = await _productService.GetById(id);

                if (hotel is null)
                {
                    return NotFound();
                }
                return Ok(hotel);
            }

            [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(nameof(AddProduct))]
        public async Task<  IActionResult> AddProduct([FromBody] Product product)
        {
            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(product);

            if (validationResult.IsValid)
            {
                _productService.Add(product);
                return Ok("create succesfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }


        }



        [HttpPut("UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id <= 0)
                return BadRequest("Not a valid   id");

            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(product);
            if (validationResult.IsValid)
            {
                await   _productService.Update(id, product);
                return Ok();
            }
            return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
        }


        private bool ProductExists(int id)
        {
            return (_productContext.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
         
         
    }


}





    




 
    

   


       
 
