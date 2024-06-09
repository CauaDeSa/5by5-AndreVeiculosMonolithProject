using System.ComponentModel.DataAnnotations;

namespace Models.Cars;

public class Car
{
    public static readonly string SelectAll = "SELECT * FROM Car";
    public static readonly string Select = "SELECT * FROM Car WHERE Plate = @plate";
    public static readonly string Insert = "INSERT INTO Car (Plate, Name, ModelYear, ManufactureYear, Color, Sold) OUTPUT INSERTED.Plate VALUES (@plate, @name, @modelYear, @manufactureYear, @color, @sold)";

    [Key]
    public string Plate { get; set; }
    public string Name { get; set; }
    public int ModelYear { get; set; }
    public int ManufactureYear { get; set; }
    public string Color { get; set; }
    public bool Sold { get; set; }
}