using Models.Cars;
using Models.People;

namespace Models.Profitable;

public class Sale
{
    public int Id { get; set; }
    public Car Car { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal SalePrice { get; set; }
    public Customer Customer { get; set; }
    public Employee Employee { get; set; }
    public Payment Payment { get; set; }
}