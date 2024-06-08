using System.ComponentModel.DataAnnotations;

namespace Models.Cars;

public class Car
{
    [Key]
    public string Plate { get; set; }
    public string Name { get; set; }
    public int ModelYear { get; set; }
    public int ManufactureYear { get; set; }
    public string Color { get; set; }
    public bool Sold { get; set; }
}