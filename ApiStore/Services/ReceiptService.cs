using ApiStore.Dtos;
using ApiStore.Models;
using ApiStore.Repositories;

namespace ApiStore.Services;

public interface IReceiptService
{
    public IEnumerable<Receipt> GetAll();
    public Receipt GetDetails(int id);
    public Receipt Open();
    public void Close(int id);
    public Receipt Add(DetailReceiptRequestDto request);
}


public class ReceiptService : IReceiptService
{
    private readonly IReceiptRepository _receiptRepository;
    private readonly IProductRepository _productRepository;
    public ReceiptService(IReceiptRepository receiptRepository,IProductRepository productRepository)
    {
        _receiptRepository = receiptRepository;
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetAll()
    {
        return null;
    }

    public Receipt GetDetails(int id)
    {
        return null;
    }
    
    public Receipt Open()
    {
        return null;
    }

    public void Close(int id)
    {
    }

    public Receipt Add(int id, DetailReceiptRequestDto request) 
    {
        Receipt receipt = _receiptRepository.Get(id);

        //Checar que si existe ese producto(Ejecutar checar)
        var exists = _productRepository.Check(request.NameProduct);´//pedimos el id del producto?

        DetailReceipt DetailAdded = new DetailReceipt
        {
            NameProduct = request.NameProduct,
            Amount = request.Amount
        };

        receipt.ReceiptDetails.Append(DetailAdded);

        return receipt;
    }

}
