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
    public Receipt Update(Receipt receipt);
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

        var csvRecords = readerCsv.GetRecords<ReceiptCSV>().ToList();
        return csvRecords.Select(csv => new Receipt
        {
            Id = csv.Id,
            Date = csv.Date,
            NameClient = csv.NameClient,
            DocClient = csv.DocClient,
            Estado = csv.Estado,
            Total = csv.Total,
            
            ReceiptDetails = string.IsNullOrWhiteSpace(csv.ReceiptDetailsId)
                ? new List<DetailReceipt>()
                : csv.ReceiptDetailsId.Split('|')
                    .Select(idStr => new DetailReceipt{ Id = int.Parse(idStr) })
                    .ToList()
        }).ToList();
    }

    public Receipt Get(int id)
    {
        var receipt = GetAll().FirstOrDefault(r => r.Id == id);
        if (receipt is null)
        {
            return null;
        }
        return receipt;
    }

    public void Save(Receipt receipt)
    {
        var exists = File.Exists(_path);

        var receiptCsv = new ReceiptCSV
        {
            Id = receipt.Id,
            Date = receipt.Date,
            NameClient = receipt.NameClient,
            DocClient = receipt.DocClient,
            Estado = receipt.Estado,
            Total = receipt.Total,
            ReceiptDetailsId = receipt.ReceiptDetails != null && receipt.ReceiptDetails.Any()
                ? string.Join("|", receipt.ReceiptDetails.Select(d => d.Id))
                : string.Empty
        };

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = !exists
        };

        using (var writer = new StreamWriter(_path, append: true))
        using (var writerCsv = new CsvWriter(writer, config))
        {
            writerCsv.WriteRecords(new[] { receiptCsv });
        }
    }

    private void SaveClean(IEnumerable<Receipt> receipts)
    {
        var csvRecords = receipts.Select(r => new ReceiptCSV
        {
            Id = r.Id,
            Date = r.Date,
            NameClient = r.NameClient,
            DocClient = r.DocClient,
            Estado = r.Estado,
            Total = r.Total,
            ReceiptDetailsId = r.ReceiptDetails != null && r.ReceiptDetails.Any()
                ? string.Join("|", r.ReceiptDetails.Select(d => d.Id))
                : string.Empty
        }).ToList();

        using (var writer = new StreamWriter(_path))
        using (var writerCsv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            writerCsv.WriteRecords(csvRecords);
        }
    }

    public Receipt Update(Receipt receipt)
    {
        var receipts = GetAll();

        receipts = receipts.Select(r => r.Id == receipt.Id ? receipt : r);
 
        SaveClean(receipts);

        return receipt;
    }
}
internal class ReceiptCSV
{
    public int Id { get; set; } 
    public DateTime Date { get; set; } 
    public string NameClient { get; set; }
    public string DocClient { get; set; }
    public bool Estado { get; set; } = true;
    public string ReceiptDetailsId { get; set; }
    public decimal Total { get; set; } 
}