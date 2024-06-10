using Models.Cars;

namespace Models.CarsDTO
{
    public class CarOperationDTO
    {
        public string CarPlate { get; set; }
        public int OperationId { get; set; }
        public bool OperationStatus { get; set; }
    }
}