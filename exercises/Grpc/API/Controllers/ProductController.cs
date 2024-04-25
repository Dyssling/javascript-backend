using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly string _address = "https://localhost:7015";
        private readonly Product.ProductClient _client;

        public ProductController()
        {
            _channel = GrpcChannel.ForAddress(_address);
            _client = new Product.ProductClient(_channel);

        }

        [HttpPost]

        public async Task<IActionResult> Add(ProductObject product)
        {
            var response = await _client.AddProductAsync(product);

            return CreatedAtAction("Add", response);
        }

        [HttpGet]

        public async Task<IActionResult> Get(string name)
        {
            var props = new ProductProps() { Name = name };
            var response = await _client.GetProductAsync(props);

            return Ok(response);
        }
    }
}
