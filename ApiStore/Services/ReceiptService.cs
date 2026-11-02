using ApiStore.Controllers.DTOS;
using ApiStore.Models;
using ApiStore.Repositories;

namespace ApiStore.Services;

public interface IReceiptService
{
    public IEnumerable<Receipt> GetAll();
    public Receipt Get(int id);
    public Receipt Create(ReceiptRequestDTO request);
    public Receipt Close(int id);
}
public class ReceiptService : IReceiptService
{
    private readonly IReceiptRepository _receiptRepository;
    public ReceiptService(IReceiptRepository receiptRepository)
    {
        _receiptRepository = receiptRepository;
    }

    public IEnumerable<Receipt> GetAll()
    {
        return _receiptRepository.GetAll();
    }

    public Receipt Get(int id)
    {
        return _receiptRepository.Get(id);
    }

    public Receipt Create(ReceiptRequestDTO request)
    {
        var newReceipt = new Receipt
        {
            NameClient = request.NameClient,
            DocClient = request.DocClient,
            ReceiptDetails = new List<DetailReceipt>(),
        };
        _receiptRepository.Save(newReceipt);
        return newReceipt;
    }

    public Receipt Close(int id)
    {
        var receipt = _receiptRepository.Get(id);
        receipt.Estado = false;
        return _receiptRepository.Update(receipt);
    }
}
