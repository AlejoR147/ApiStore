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
    public Receipt Close(int id);
}
public class ReceiptService : IReceiptService
{
    private readonly IReceiptRepository _receiptRepository;
    private readonly IProductService _productService;
    private readonly IDetailReceiptService _detailReceiptService;

    public ReceiptService(IReceiptRepository receiptRepository, IProductService productService, IDetailReceiptService detailReceiptService)
    {
        _receiptRepository = receiptRepository;
        _detailReceiptService = detailReceiptService;
        _productService = productService;
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
        var product = _productService.Get(request.productId);

        if (receipt.Estado == false) 
        { throw new Exception("la factura se encuentra cerrada"); }
        if (product.Stock < request.Amount) 
        { throw new Exception("No hay stock suficiente"); }


        var newDetail = _detailReceiptService.Create(request, product);

        UpdateReceipt(receipt, newDetail);
         //pendiente agregar a la carpeta de details
        _receiptRepository.SaveDetail(id,newDetail);
        _productService.UpdateStock(request.productId, request.Amount);


        return receipt;
    }
    private Receipt UpdateReceipt(Receipt receipt, DetailReceipt newDetail)
    {
        receipt.ReceiptDetails = receipt.ReceiptDetails.Append(newDetail).ToList();
        receipt.Total = receipt.ReceiptDetails.Sum(d => d.Subtotal);

        return _receiptRepository.Update(receipt);
    }

    public Receipt Close(int id)
    {
        var receipt = _receiptRepository.Get(id);
        if (receipt.ReceiptDetails.Count == 0)
        {
            throw new Exception("No se puede cerrar la factura, no tiene detalles");
        }
        if (receipt.Estado == false)
        {
            throw new Exception("La factura ya se encuentra cerrada");
        }
        receipt.Estado = false;
        return _receiptRepository.Update(receipt);
    }
}
