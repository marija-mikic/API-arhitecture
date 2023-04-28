using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi_DAL.Models;

namespace WebApi_DAL.Repository
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        bool Add(Product model);
        bool Update(Product model);
        bool Delete(int Id);
        Product GetById(int Id);
    }
}
