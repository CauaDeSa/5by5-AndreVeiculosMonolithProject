using Models.Cars;

namespace Models.CarsDTO
{
    public class CarOperationDTO
    {
        public Car Car { get; set; }
        public Operation Operation { get; set; }
        public bool OperationStatus { get; set; }

        public CarOperationDTO(string carPlate, int operationId, bool operationStatus)
        {
            Car = new() { Plate = carPlate };
            Operation = new() { Id = operationId };
            OperationStatus = operationStatus;
        }
    }
}