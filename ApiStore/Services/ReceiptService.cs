using ApiStore.Controllers.DTOS;
using ApiStore.Models;
using ApiStore.Repositories;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ApiStore.Services;

public interface IReceiptService
{
    public IEnumerable<Receipt> GetAll();
    public Receipt Get(int id);
    public Receipt AddDetail(int id, DetailRequestDTO request);
    public Receipt Create(ReceiptRequestDTO request);
    public Receipt UpdateReceipt(Receipt receipt, DetailReceipt newDetail);
    public Receipt Close(int id);
}
public class ReceiptService : IReceiptService
{
    private readonly IReceiptRepository _receiptRepository;
    private readonly IProductRepository _productRepository;
    public ReceiptService(IReceiptRepository receiptRepository, IProductRepository productRepository)
    {
        _receiptRepository = receiptRepository;
        _productRepository = productRepository;
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


    public Receipt AddDetail(int id, DetailRequestDTO request)
    {
        var receipt = _receiptRepository.Get(id);
        var product = _productRepository.Get(request.productId);

        if (receipt == null) { throw new Exception("la factura no existe o se encuentra cerrada"); }

        if (product != null && product.Stock >= request.Amount)
        {
            var newDetail = new DetailReceipt { productId = request.productId, ReceiptId = id, Amount = request.Amount, UnitPrice = product.Price, Subtotal = (int)(product.Price * request.Amount) };


            UpdateReceipt(receipt, newDetail);
            //pendiente agregar a la carpeta de details
            _receiptRepository.SaveDetail(id,newDetail);
            _productRepository.UpdateStock(request.productId, request.Amount);
        }

        return receipt;

    }
    private Receipt UpdateReceipt(Receipt receipt, DetailReceipt newDetail)
    {
        receipt.ReceiptDetails = receipt.ReceiptDetails.Append(newDetail).ToList();
        receipt.Total = receipt.ReceiptDetails.Sum(d => d.Subtotal);

        return receipt;
    }



    public Receipt Close(int id)
    {
        var receipt = _receiptRepository.Get(id);
        receipt.Estado = false;
        return _receiptRepository.Update(receipt);
    }
}
