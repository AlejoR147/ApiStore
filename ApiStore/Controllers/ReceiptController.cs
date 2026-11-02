using ApiStore.Dtos;
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

    [HttpPut]
    public ReceiptResponseDto AddProduct(int id, DetailReceiptRequestDto request)
    {
        var receipts = _receiptService.Add();


    }




}
