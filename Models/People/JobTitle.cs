namespace Models.People;

public class JobTitle
{
    public static readonly string Select = "SELECT * FROM JobTitle WHERE Id = @id";
    public static readonly string Insert = "SELECT * FROM JobTitle WHERE Id = @id";

    public int Id { get; set; }
    public string Description { get; set; }
}