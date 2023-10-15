using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;
using Mango.Services.ProductAPI.Repository.Interfaces;
using Mango.Services.ProductAPI.Service.Interface;

namespace Mango.Services.ProductAPI.Service;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> CreateProduct(AddProductDto product)
    {
        try
        {
            var mapped = _mapper.Map<Product>(product);
            mapped.ProductId = Guid.NewGuid();

            _unitOfWork.ProductRepository.Add(mapped);
            bool result = await _unitOfWork.Complete();

            if (result)
            {
                return _mapper.Map<ProductDto>(mapped);
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        try
        {
            var response = await _unitOfWork.ProductRepository.Get(productId) ?? throw new Exception($"Product not found for id: {productId}");
            _unitOfWork.ProductRepository.Delete(response);
            return await _unitOfWork.Complete();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        try
        {
            var response = await _unitOfWork.ProductRepository.GetAll();

            if(response == null)
            {
                return Enumerable.Empty<ProductDto>();
            }

            return _mapper.Map<IEnumerable<ProductDto>>(response.OrderBy(s => s.UpdatedAt));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ProductDto> GetProductById(Guid productId)
    {
        try
        {
            var response = await _unitOfWork.ProductRepository.Get(productId) ?? throw new Exception($"Product not found for id: {productId}");
            
            return _mapper.Map<ProductDto>(response);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ProductDto> UpdateProduct(ProductDto product)
    {
        try
        {
            var response = await _unitOfWork.ProductRepository.Get(product.ProductId) ?? throw new Exception($"Product not found for id: {product.ProductId}");

            response.Name = product.Name;
            response.Description = product.Description;
            response.Price = product.Price;
            response.CategoryName = product.CategoryName;
            response.ImageUrl = product.ImageUrl;
            response.UpdatedAt = DateTime.Now;

            _unitOfWork.ProductRepository.Update(response);
            bool result = await _unitOfWork.Complete();

            if(result)
            {
                return _mapper.Map<ProductDto>(response);
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
