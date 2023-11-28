using CatalogApi.Data;
using CatalogApi.Entities;
using MongoDB.Driver;

namespace CatalogApi.Repositories
{
    public class ProductRepository(ICatalogContext catalogContext) : IProductRepository
    {

        public async Task CreateProduct(Product product)
         => await catalogContext.Products.InsertOneAsync(product);

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            DeleteResult deleteResult = await catalogContext.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public Task<Product> GetProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
