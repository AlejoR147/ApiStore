using ApiStore.Repositories;

namespace ApiStore.Services;

public interface IReceiptService
{
}
public class ReceiptService : IReceiptService
{
    private readonly IReceiptRepository _receiptRepository;
    public ReceiptService(IReceiptRepository receiptRepository)
    {
        _receiptRepository = receiptRepository;
    }
}
