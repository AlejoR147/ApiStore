using ApiStore.Repositories;

namespace ApiStore.Services;
public interface IDetailReceiptService
{
}
public class DetailReceiptService : IDetailReceiptService
{
    private readonly IDetailReceiptRepository _detailReceiptRepository;
    public DetailReceiptService(IDetailReceiptRepository detailReceiptRepository)
    {
        _detailReceiptRepository = detailReceiptRepository;
    }
}
