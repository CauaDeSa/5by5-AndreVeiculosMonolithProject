using System.ComponentModel.DataAnnotations;

namespace Models.Profitable;

public class Fetlock
{
    public int Id { get; set; }
    public int Number { get; set; }
    public DateTime DueDate { get; set; }
}