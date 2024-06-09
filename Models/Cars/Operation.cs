using Models.CarsDTO;

namespace Models.Cars;

public class Operation
{
    public int Id { get; set; }
    public string Description { get; set; }

    public Operation() { }

    public Operation(OperationDTO dto) 
    {
        Description = dto.Description;
    }
}