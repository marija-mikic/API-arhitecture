using WebApi_DAL.Models;

namespace WebApi_DAL.Repository
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAll();
        bool Add(Product model);

        bool Delete(int Id);
        Product GetById(int Id);

       Task Update(int id );
         
    }
}
