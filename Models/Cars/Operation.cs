using Models.CarsDTO;

namespace Models.Cars;

public class Operation
{
    public static readonly string SelectAll = "SELECT * FROM Operation";
    public static readonly string Select = "SELECT * FROM Operation WHERE Id = @id";
    public static readonly string Insert = "INSERT INTO Operation (Description) OUTPUT INSERTED.Id VALUES (@description)";

    public int Id { get; set; }
    public string Description { get; set; }

    public Operation() { }

    public Operation(OperationDTO dto) 
    {
        Description = dto.Description;
    }
}