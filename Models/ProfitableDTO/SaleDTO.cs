using Models.Cars;
using Models.People;
using Models.Profitable;

namespace Models.ProfitableDTO
{
    public class SaleDTO
    {
        public Car Car { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public Payment Payment { get; set; }

        public SaleDTO(string carPlate, DateTime saleDate, decimal salePrice, string customerDocument, string employeeDocument, int paymentId)
        {
            this.Car = new() { Plate = carPlate };
            this.SaleDate = saleDate;
            this.SalePrice = salePrice;
            this.Customer = new() { Document = customerDocument };
            this.Employee = new() { Document = employeeDocument };
            this.Payment = new() { Id = paymentId };
        }
    }
}