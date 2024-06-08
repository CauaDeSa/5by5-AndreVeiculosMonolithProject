using System.ComponentModel.DataAnnotations;

namespace Models.Profitable;

public class Card
{
    [Key]
    public string CardNumber { get; set; }
    public string SecurityKey { get; set; }
    public string DueDate { get; set; }
    public string CardName { get; set; }
}