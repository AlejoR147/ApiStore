using ApiStore.Controllers.DTOS;
using ApiStore.Models;
using ApiStore.Repositories;

namespace ApiStore.Services;
public interface IDetailReceiptService
{
    public DetailReceipt Create(DetailRequestDTO request, Product product);
    public IEnumerable<DetailReceipt> GetAll();
}
public class DetailReceiptService : IDetailReceiptService
{
    private readonly IDetailReceiptRepository _detailReceiptRepository;
    public DetailReceiptService(IDetailReceiptRepository detailReceiptRepository)
    {
        _detailReceiptRepository = detailReceiptRepository;
    }

    public DetailReceipt Create(DetailRequestDTO request, Product product)
    {
        var newDetail = new DetailReceipt
        {
            ProductId = request.productId,
            Description = product.Description,
            Amount = request.Amount,
            UnitPrice = product.Price,
            Subtotal = (product.Price * request.Amount)
        };
        _detailReceiptRepository.Save(newDetail);
        return newDetail;
    }

    public IEnumerable<DetailReceipt> GetAll()
    {
        return _detailReceiptRepository.GetAll();
    }
}
