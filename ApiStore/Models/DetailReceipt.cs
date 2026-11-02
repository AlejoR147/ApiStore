namespace ApiStore.Models;

public class DetailReceipt
{
    public int Id { get; set; } = Random.Shared.Next();
    public string NameProduct { get; set; }
    public string Amount { get; set; }

}

