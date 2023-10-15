using Mango.Services.ProductAPI.Models.Dtos;

namespace Mango.Services.ProductAPI.Service.Interface;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts();

    Task<ProductDto> GetProductById(Guid productId);

    Task<ProductDto> CreateProduct(AddProductDto product);

    Task<ProductDto> UpdateProduct(ProductDto product);

    Task<bool> DeleteProduct(Guid productId);
}
