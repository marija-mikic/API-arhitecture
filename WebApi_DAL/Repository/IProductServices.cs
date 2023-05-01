using WebApi_DAL.Models;

namespace WebApi_DAL.Repository
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAll();
        bool Add(Product model);

        bool Delete(int Id);
       

        Task Update(int id, Product product );
        Task<Product> GetById(int id);
    }
}
