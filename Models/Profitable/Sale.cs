using Models.Cars;
using Models.People;
using Models.ProfitableDTO;

namespace Models.Profitable;

public class Sale
{
    public int Id { get; set; }
    public Car Car { get; set; }
    public DateTime SaleDate { get; set; }
    public double SalePrice { get; set; }
    public Customer Customer { get; set; }
    public Employee Employee { get; set; }
    public Payment Payment { get; set; }

    public Sale() { }

    public Sale(SaleDTO saleDTO)
    {
        this.Car = new() { Plate = saleDTO.CarPlate };
        this.SaleDate = saleDTO.SaleDate;
        this.SalePrice = saleDTO.SalePrice;
        this.Customer = new() { Document = saleDTO.CustomerDocument };
        this.Employee = new() { Document = saleDTO.EmployeeDocument };
        this.Payment = new() { Id = saleDTO.PaymentId };
    }
}