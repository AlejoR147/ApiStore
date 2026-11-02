using ApiStore.Models;

namespace ApiStore.Controllers.DTOS;

public class ReceiptResponseDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string NameClient { get; set; }
    public string DocClient { get; set; }
    public bool Estado { get; set; }
    public List<DetailReceipt> ReceiptDetails { get; set; }
    public decimal Total { get; set; }
}
        
