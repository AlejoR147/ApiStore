using ApiStore.Repositories;

namespace ApiStore.Services;

public interface IProductService
{
}
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
}
