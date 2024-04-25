using Grpc.Core;
using GrpcServer;
using GrpcServer.Contexts;
using GrpcServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Services
{
    public class ProductService : Product.ProductBase
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }

        public override async Task<AddProductResponse> AddProduct(ProductObject product, ServerCallContext context)
        {
            try
            {
                await _context.AddAsync(new ProductEntity() { Name = product.Name, Description = product.Description });
                return (new AddProductResponse
                {
                    Status = "Ok"
                });
            }

            catch { }

            return (new AddProductResponse
            {
                Status = "Error"
            });
        }

        public override Task<GetProductResponse> GetProduct(ProductProps props, ServerCallContext context)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.Name == props.Name);

                return Task.FromResult(new GetProductResponse()
                {
                    Status = "Ok",
                    Name = product!.Name,
                    Description = product.Description
                });
            }

            catch { }

            return Task.FromResult(new GetProductResponse
            {
                Status = "Error"
            });
        }
    }
}
