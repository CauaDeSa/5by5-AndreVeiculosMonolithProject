using Models.Cars;
using Models.People;
using Models.Profitable;

namespace Models.ProfitableDTO
{
    public class SaleDTO
    {
        public string CarPlate { get; set; }
        public DateTime SaleDate { get; set; }
        public double SalePrice { get; set; }
        public string CustomerDocument { get; set; }
        public string EmployeeDocument { get; set; }
        public int PaymentId { get; set; }

        public SaleDTO(string carPlate, DateTime saleDate, double salePrice, string customerDocument, string employeeDocument, int paymentId)
        {
            this.CarPlate = carPlate;
            this.SaleDate = saleDate;
            this.SalePrice = salePrice;
            this.CustomerDocument = customerDocument;
            this.EmployeeDocument = employeeDocument;
            this.PaymentId = paymentId;
        }
    }
}