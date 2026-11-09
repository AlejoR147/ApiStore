using ApiStore.Controllers.DTOS;
using ApiStore.Models;
using ApiStore.Repositories;

namespace ApiStore.Services;
public interface IDetailReceiptService
{
    public DetailReceipt Create(DetailRequestDTO request, Product product);
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
            productId = request.productId,
            Amount = request.Amount,
            UnitPrice = product.Price,
            Subtotal = (product.Price * request.Amount)
        };
        _detailReceiptRepository.Save(newDetail);
        return newDetail;
    } 
}
