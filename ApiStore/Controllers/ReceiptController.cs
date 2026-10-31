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
}
