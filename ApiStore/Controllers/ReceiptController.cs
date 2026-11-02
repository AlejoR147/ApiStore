using ApiStore.Controllers.DTOS;
using ApiStore.Models;
using ApiStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiStore.Controllers;

[ApiController]
[Route("/receipts")]
public class ReceiptController
{
    private readonly IReceiptService _receiptService;

    public ReceiptController(IReceiptService receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpGet("all/")]
    public IEnumerable<ReceiptResponseDTO> GetAll()
    {
        var receipts = _receiptService.GetAll();
        return receipts.Select(r => new ReceiptResponseDTO
        {
            Id = r.Id,
            Date = r.Date,
            NameClient = r.NameClient,
            DocClient = r.DocClient,
            Estado = r.Estado,
            ReceiptDetails = r.ReceiptDetails,
            Total = r.Total
        });
    }

    [HttpGet()]
    public ReceiptResponseDTO Get(int id)
    {
        var clientFind = _receiptService.Get(id);
        if (clientFind is null)
        {
            return null;
        }
        return new ReceiptResponseDTO
        {
            Id = clientFind.Id,
            Date = clientFind.Date,
            NameClient = clientFind.NameClient,
            DocClient = clientFind.DocClient,
            Estado = clientFind.Estado,
            ReceiptDetails = clientFind.ReceiptDetails,
            Total = clientFind.Total
        };
    }

    [HttpPost()]
    public ReceiptResponseDTO OpenReceipt(ReceiptRequestDTO request)
    {
        var newReceipt = _receiptService.Create(request);
        return new ReceiptResponseDTO
        {
            Id = newReceipt.Id,
            Date = newReceipt.Date,
            NameClient = newReceipt.NameClient,
            DocClient = newReceipt.DocClient,
            Estado = newReceipt.Estado,
            ReceiptDetails = newReceipt.ReceiptDetails,
            Total = newReceipt.Total
        };
    }
    [HttpPost("close/")]
    public ReceiptResponseDTO CloseReceipt(int id) 
    {
        var receipt = _receiptService.Close(id);
        return new ReceiptResponseDTO
        {
            Id = receipt.Id,
            Date = receipt.Date,
            NameClient = receipt.NameClient,
            DocClient = receipt.DocClient,
            Estado = receipt.Estado,
            ReceiptDetails = receipt.ReceiptDetails,
            Total = receipt.Total
        };
    }
}
