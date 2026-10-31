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

    
}
