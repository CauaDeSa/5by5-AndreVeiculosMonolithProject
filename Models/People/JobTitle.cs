namespace Models.People;

public class JobTitle
{
    public static readonly string SelectAll = "SELECT * FROM JobTitle";
    public static readonly string Select = "SELECT * FROM JobTitle WHERE Id = @id";
    public static readonly string Insert = "INSERT INTO JobTitle (Description) OUTPUT INSERTED.Id VALUES (@description)";

    public int Id { get; set; }
    public string Description { get; set; }
}