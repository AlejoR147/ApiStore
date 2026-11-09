using CsvHelper;
using CsvHelper.Configuration;
using ApiStore.Controllers;
using ApiStore.Models;
using ApiStore.Repositories;
using System.Globalization;


namespace ApiStore.Services;

public interface IProductService
{
    public IEnumerable<Product> GetAll();
    public Product Get(int id);
    public Product UpdateStock(int productId, int amount);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetAll()
    {
        return _productRepository.GetAll();

    }

    public Product Get(int id)
    {
        return _productRepository.Get(id);
    }

    public Product UpdateStock(int productId, int amount)
    {

        return _productRepository.UpdateStock(productId, amount);
    }
}
