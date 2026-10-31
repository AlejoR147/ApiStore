using ApiStore.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace ApiStore.Repositories;

public interface IReceiptRepository
{
    public IEnumerable<Receipt> GetAll();
    public Receipt? Get(int id);
    public void Save(Receipt receipt);
}
public class ReceiptRepository : IReceiptRepository
{
    private const string _path = "./CSV/receipts.csv";

    public IEnumerable<Receipt> GetAll()
    {
        var exists = File.Exists(_path);

        if (!exists)
        {
            return null;
        }

        using var reader = new StreamReader(_path);
        using var readerCsv = new CsvReader(reader, CultureInfo.InvariantCulture);

        return readerCsv.GetRecords<Receipt>().ToList();
    }

    public Receipt? Get(int id)
    {
        var clients = GetAll();

        return clients.FirstOrDefault(); //configurar
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

    private void SaveClean(IEnumerable<Receipt> receipts)
    {
        using (var writer = new StreamWriter(_path))
        using (var writerCsv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            writerCsv.WriteRecords(receipts);
        }
    }
}
