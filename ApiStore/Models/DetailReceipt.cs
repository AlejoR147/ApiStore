namespace ApiStore.Models;

public class DetailReceipt
{
    public int productId { get; set; }
    public int ReceiptId { get; set; }
    public int Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public int Subtotal { get; set; }
}

