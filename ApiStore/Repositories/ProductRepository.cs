using ApiStore.Models;
using CsvHelper;
using System.Globalization;

namespace ApiStore.Repositories;

public interface IProductRepository
{
    public IEnumerable<Product> GetAll();
    public Product? Get(int id);
}
public class ProductRepository : IProductRepository
{
    private const string _path = "./CSV/products.csv";
    public IEnumerable<Product> GetAll()
    {
        var exists = File.Exists(_path);

        if (!exists)
        {
            return null;
        }

        using var reader = new StreamReader(_path);
        using var readerCsv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return readerCsv.GetRecords<Product>().ToList();
    }

    public Product? Get(int id)
    {
        var clients = GetAll();

        return clients.FirstOrDefault(c => c.Id == id);
    }
}
