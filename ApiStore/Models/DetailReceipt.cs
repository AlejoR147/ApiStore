namespace ApiStore.Models;

public class DetailReceipt
{
    public int Id{ get; set; } = new Random().Next();
    public int ProductId { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}

