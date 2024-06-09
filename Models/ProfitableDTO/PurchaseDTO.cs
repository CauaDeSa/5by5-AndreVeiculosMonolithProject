using Models.Cars;

namespace Models.ProfitableDTO
{
    public class PurchaseDTO
    {
        public string CarPlate { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }

        public PurchaseDTO(string carPlate, double price, DateTime purchaseDate)
        {
            this.CarPlate = carPlate;
            this.Price = price;
            this.PurchaseDate = purchaseDate;
        }
    }
}