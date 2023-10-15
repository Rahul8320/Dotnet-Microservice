using Mango.Services.ProductAPI.Models.Dtos;
using Mango.Services.ProductAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/product")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ResponseDto _response;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ResponseDto response, ILogger<ProductController> logger)
    {
        _productService = productService;
        _response = response;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ResponseDto> Get()
    {
        try
        {
            IEnumerable<ProductDto> productList = await _productService.GetAllProducts();
            if (productList == null)
            {
                _logger.LogInformation("No Products found!");
                _response.IsSuccess = false;
                _response.Message = "No Products Found!";
            }
            else
            {
                _logger.LogInformation("Get products List: {@objList}", productList);
                _response.Result = productList;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<ResponseDto> Get(Guid id)
    {
        try
        {
            ProductDto product = await _productService.GetProductById(id);
            if (product == null)
            {
                _response.IsSuccess = false;
                _logger.LogInformation("No Product Found with id: {@id}", id);
                _response.Message = $"No Product Found with id: {id}";
            }
            _logger.LogInformation("Get Product: {@objList}", product);
            _response.Result = product;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Post(AddProductDto product)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(product);
                _logger.LogInformation("Product Created {@response}", response);
                _response.Result = response;
            }
            else
            {
                _response.IsSuccess = false;
                _response.Result = ModelState;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Put(ProductDto product)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(product);
                _logger.LogInformation("Product Updated {@response}", response);
                _response.Result = response;
            }
            else
            {
                _response.IsSuccess = false;
                _response.Result = ModelState;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }

    [HttpDelete]
    [Authorize(Roles = "ADMIN")]
    public async Task<ResponseDto> Delete(Guid id)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProduct(id);

                if(response == true)
                {
                    _logger.LogInformation("Product with id: {@id} Deleted Successfully!", id);
                    _response.Message = $"Product with id: {id} Deleted Successfully!";
                }

                _response.Result = response;
            }
            else
            {
                _response.IsSuccess = false;
                _response.Result = ModelState;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            _response.IsSuccess = false;
            _response.Message = ex.Message;
        }
        return _response;
    }
}
