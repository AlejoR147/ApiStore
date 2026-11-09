using ApiStore.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ApiStore.Repositories;
public interface IDetailReceiptRepository
{
}
public class DetailReceiptRepository : IDetailReceiptRepository
{
    private const string _path = "./CSV/detail_receipts.csv";

    public IEnumerable<DetailReceipt> GetAll()
    {
        var exists = File.Exists(_path);

        if (!exists)
        {
            return null;
        }

        using var reader = new StreamReader(_path);
        using var readerCsv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return readerCsv.GetRecords<DetailReceipt>().ToList();
    }

    public DetailReceipt Get(int id)
    {
        var receipt = GetAll().FirstOrDefault(r => r.productId == id);
        if (receipt is null)
        {
            return null;
        }
        return receipt;
    }

    public void Save(Receipt receipt)
    {
        var exists = File.Exists(_path);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = !exists
        };

        using (var writer = new StreamWriter(_path, append: true))
        using (var writerCsv = new CsvWriter(writer, config))
        {
            writerCsv.WriteRecords(new[] { receipt });
        }
    }
}
