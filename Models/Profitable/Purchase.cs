using Models.Cars;
using Models.ProfitableDTO;

namespace Models.Profitable;

public class Purchase
{
    public int Id { get; set; }
    public Car Car { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }

    public Purchase(PurchaseDTO purchaseDTO)
    {
        Car = purchaseDTO.Car;
        Price = purchaseDTO.Price;
        PurchaseDate = purchaseDTO.PurchaseDate;
    }
}