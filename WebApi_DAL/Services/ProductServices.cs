using Microsoft.EntityFrameworkCore;
using WebApi_DAL.Models;
using WebApi_DAL.Pagination;
using WebApi_DAL.Repository;
 

namespace WebApi_DAL.Services
{


    public class ProductServices : IProductServices
    {
        private readonly ProductContext context;
       
         


        public ProductServices(ProductContext context)
        {
            this.context = context;
             
           
        }

        public bool Add(Product model)
        {
            try
            {
                context.Products.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int Id)
        {
            try
            {
                var data = ((IProductServices)this).GetById(Id);
                if (data == null)
                    return false;
                context.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PagedList<Product>> GetAll(Paging paging)
        {
            return await context.Products
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Title = p.Title,
                        Description = p.Description
                    })
                .AsNoTracking()
                .ToListAsync();

            return PagedList<Product>.ToPagedList(paging.PageSize, paging.maxPageSize);
             

         }
             
    



        public async Task Update(int id,Product product)
        {
            await context.Products
                .Where(p => p.Id.Equals(id))
                .ExecuteUpdateAsync(p =>
                p.SetProperty(p => p.Name, p => product.Name)
                .SetProperty(p => p.Title, p => product.Title)
                .SetProperty(p => p.Description, p => product.Description)); 
        }

        public async Task<Product>GetById(int id)
        {
            return await context.Products
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Title = p.Title,
                    Description = p.Description
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x=>x.Id==id);



        }
         


    }

}
