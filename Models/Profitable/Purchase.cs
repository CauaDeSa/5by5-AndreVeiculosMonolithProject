using Models.Cars;
using Models.ProfitableDTO;

namespace Models.Profitable;

public class Purchase
{
    public static readonly string SelectAll = "SELECT * FROM Purchase";
    public static readonly string Select = "SELECT * FROM Purchase WHERE Id = @id";
    public static readonly string Insert = "INSERT INTO Purchase (CarPlate, Price, PurchaseDate) OUTPUT INSERTED.Id VALUES (@carPlate, @price, @purchaseDate)";

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