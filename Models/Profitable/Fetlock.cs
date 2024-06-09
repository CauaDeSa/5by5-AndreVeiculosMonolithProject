using System.ComponentModel.DataAnnotations;

namespace Models.Profitable;

public class Fetlock
{
    public static readonly string Select = "SELECT * FROM Fetlock WHERE Id = @fetlockId";
    public static readonly string Insert = "INSERT INTO Fetlock (Number, DueDate) OUPTPUT INSERTED.Id VALUES (@Number, @DueDate)";

    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime DueDate { get; set; }
}