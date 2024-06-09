using Models.Cars;

namespace Models.ProfitableDTO
{
    public class PurchaseDTO
    {
        public string CarPlate { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}