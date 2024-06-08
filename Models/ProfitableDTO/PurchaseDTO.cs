using Models.Cars;

namespace Models.ProfitableDTO
{
    public class PurchaseDTO
    {
        public Car Car { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }

        public PurchaseDTO(string carPlate, decimal price, DateTime purchaseDate)
        {
            this.Car = new() { Plate = carPlate};
            this.Price = price;
            this.PurchaseDate = purchaseDate;
        }
    }
}