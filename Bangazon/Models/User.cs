namespace Bangazon.Models;

public class User
{  
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public bool isSeller { get; set; }
    public ICollection<Order> orders { get; set; }
    public ICollection<PaymentType> paymentTypes { get; set; }
}