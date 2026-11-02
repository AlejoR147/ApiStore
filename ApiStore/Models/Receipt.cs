namespace ApiStore.Models;

public class Receipt
{
    public int Id { get; set; } = new Random().Next();
    public DateTime Date { get; set; } = DateTime.Now;
    public string NameClient { get; set; }
    public string DocClient { get; set; }
    public bool Estado { get; set; } = true;
    public List<DetailReceipt> ReceiptDetails { get; set; } = new();
    public decimal Total { get; set; } = 0;
}
