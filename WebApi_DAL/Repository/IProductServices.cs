using Microsoft.VisualStudio.Services.WebApi;
using WebApi_DAL.Models;
using WebApi_DAL.Pagination;

namespace WebApi_DAL.Repository
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAll(Paging paging);
        bool Add(Product model);

        bool Delete(int Id);
        
        Task Update(int id, Product product );
        Task<Product> GetById(int id);
    }
}
