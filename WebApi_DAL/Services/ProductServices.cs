using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebApi_DAL.Models;
using WebApi_DAL.Repository;

namespace WebApi_DAL.Services
{


    public class ProductServices:IProductServices 
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
                var data = this.GetById(Id);
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

        public async Task<IEnumerable<Product>> GetAll()
        {
            if (context.Products == null)
            {
                return NotFound();

            }
            return await context.Products.ToListAsync();
        }

        private IEnumerable<Product> NotFound()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int Id)
        {
            return context.Products.Find(Id);
        }



        public bool Update(int id, Product product)
        {
            try
            {
                var data = this.GetById(product.Id);
                if (data == null)
                    return false;
                context.Update(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }





    }

}
