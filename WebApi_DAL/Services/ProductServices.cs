using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_DAL.Models;
using WebApi_DAL.Repository;

namespace WebApi_DAL.Services
{


    public class ProductServices : IProductService
    {
        private readonly ProductContext context;

        public ProductServices(ProductContext context)
        {
            this.context = context;
        }
        public bool Add(Product model)
        {
            try { 
            context.Product.Add(model);
            context.SaveChanges();
            return true;
            }
            catch (Exception ){
                return false;
            }
        }
        public bool Delete(int Id)
        {
            try
            {
                var data= this.GetById(Id);
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

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int Id)
        {
            return context.Find(Id);
        }

        public bool Update(Product model)
        {
            try
            {
                context.Update(model);
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
