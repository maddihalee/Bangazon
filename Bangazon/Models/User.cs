namespace Bangazon.Models;

public class User
{  
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int CustomerId { get; set; }
    public bool isSeller { get; set; }
}