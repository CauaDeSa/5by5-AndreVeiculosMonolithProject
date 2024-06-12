namespace Models.Profitable;

public class PixType
{
    public static readonly string Select = "SELECT * FROM PixType WHERE Id = @pixTypeId";
    public static readonly string Insert = "INSERT INTO PixType (Name) OUTPUT INSERTED.Id VALUES (@Name)";

    public int Id { get; set; }
    public string Name { get; set; }
}