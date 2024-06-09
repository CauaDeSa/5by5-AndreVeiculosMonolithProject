using Models.Cars;
using Models.ProfitableDTO;

namespace Models.Profitable;

public class Purchase
{
    public int Id { get; set; }
    public Car Car { get; set; }
    public double Price { get; set; }
    public DateTime PurchaseDate { get; set; }

    public Purchase() { }

    public Purchase(PurchaseDTO purchaseDTO)
    {
        Car = new() { Plate = purchaseDTO.CarPlate };
        Price = purchaseDTO.Price;
        PurchaseDate = purchaseDTO.PurchaseDate;
    }
}