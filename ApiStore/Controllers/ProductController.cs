using ApiStore.Dtos;
using ApiStore.Models;
using ApiStore.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiStore.Controllers;

[ApiController]
[Route("/products")]
public class ProductController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    [HttpGet("all/")]
    public IEnumerable<ProductResponseDto> GetAll()
    {
        var products = _productService.GetAll();

        var productResponse = products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Stock = p.Stock

        });

        return productResponse;
    }


    [HttpGet()]
    public ProductResponseDto Get(int id)
    {
        var ProductFind = _productService.Get(id);

        if (ProductFind is null)
        {
            return new ProductResponseDto //Mejorar el manejo de errores aquí
            {
                Id = 0,
                Name = "No existe",
                Price = 0,
                Stock = 0
            };

        }
        return new ProductResponseDto
        {
            Id = ProductFind.Id,
            Name = ProductFind.Name,
            Description = ProductFind.Description,
            Price = ProductFind.Price,
            Stock = ProductFind.Stock
        };
    }


}
