using Models.CarsDTO;

namespace Models.Cars;

public class CarOperation
{
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