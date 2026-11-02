using ApiStore.Models;

namespace ApiStore.Dtos;

public class ReceiptResponseDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string clientName { get; set; }
    public int Total { get; set; }
    public DetailReceipt ReceiptDetails { get; set; }

}
