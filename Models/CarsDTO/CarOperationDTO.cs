using Models.Cars;

namespace Models.CarsDTO
{
    public class CarOperationDTO
    {
        public string CarPlate { get; set; }
        public int OperationId { get; set; }
        public bool OperationStatus { get; set; }

        public CarOperationDTO(string carPlate, int operationId, bool operationStatus)
        {
            CarPlate = carPlate;
            OperationId = operationId;
            OperationStatus = operationStatus;
        }
    }
}