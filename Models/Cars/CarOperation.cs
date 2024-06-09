using Models.CarsDTO;

namespace Models.Cars;

public class CarOperation
{
    public static readonly string Insert = "INSERT INTO CarOperation (CarPlate, OperationId, OperationStatus) OUTPUT INSERTED.ID VALUES (@carPlate, @operationId, @OperationStatus)";
    public static readonly string SelectAll = "SELECT * FROM CarOperation";
    public static readonly string Select = "SELECT * FROM CarOperation WHERE Id = @id";

    public int Id { get; set; }
    public Car Car { get; set; }
    public Operation Operation { get; set; }
    public bool OperationStatus { get; set; }

    public CarOperation() { }

    public CarOperation(CarOperationDTO carOperationDTO)
    {
        Car = new() { Plate = carOperationDTO.CarPlate };
        Operation = new() { Id = carOperationDTO.OperationId };
        OperationStatus = carOperationDTO.OperationStatus;
    }
}