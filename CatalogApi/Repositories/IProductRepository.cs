using CatalogApi.Entities;

namespace CatalogApi.Repositories
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);
        Task<IEnumerable<Product>> GetProducts();
        Task<bool> DeleteProduct(string id);
        Task<bool> UpdateProduct(Product product);
        Task<Product> GetProduct(string id);
    }
}
