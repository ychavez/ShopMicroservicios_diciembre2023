using Grpc.Core;
using InventoryGrpc.Protos;

namespace InventoryGrpc.Services
{
    public class ProductService : Existence.ExistenceBase
    {
        public override Task<ProductExistenceReply> checkExistence(ProductRequest request, ServerCallContext context)
        {
            //logica de negocios va aqui

            return Task.FromResult(new ProductExistenceReply() { ProductQTY = 12 });
        }

    }
}
