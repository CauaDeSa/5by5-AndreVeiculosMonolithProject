using System.ComponentModel.DataAnnotations;

namespace Models.Profitable;

public class Card
{
    public static readonly string Insert = "INSERT INTO Card (CardNumber, SecurityKey, DueDate, CardName) VALUES (@CardNumber, @SecurityKey, @DueDate, @CardName)";
    public static readonly string Select = "SELECT * FROM Card WHERE CardNumber = @CardNumber";

    [Key]
    public string CardNumber { get; set; }
    public string SecurityKey { get; set; }
    public string DueDate { get; set; }
    public string CardName { get; set; }
}