namespace ApiStore.Models;

public class Receipt
{

    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string clientName { get; set; }
    public int Total { get; set; }
    public DetailReceipt ReceiptDetails { get; set; }


}
