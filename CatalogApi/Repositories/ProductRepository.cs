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

        public async Task<Product> GetProduct(string id)
         => await catalogContext.Products
                                .Find(x => x.Id == id)
                                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProducts()
         => await catalogContext.Products.Find(x => true).ToListAsync();

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await catalogContext.Products
                 .ReplaceOneAsync(filter: x => x.Id == product.Id, product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

    }
}
