using Models.PeopleDTO;

namespace Models.People;

public class Address
{
    public static readonly string Insert = "INSERT INTO Address (PublicPlace, ZipCode, Neighborhood, PublicPlaceType, Number, Complement, State, City) OUTPUT INSERTED.Id VALUES (@publicPlace, @zipCode, @neighborhood, @publicPlaceType, @number, @complement, @state, @city)";

    public static readonly string Select = "SELECT * FROM Address WHERE Id = @id";

    public int Id { get; set; }
    public string PublicPlace { get; set; }
    public string ZipCode { get; set; }
    public string Neighborhood { get; set; }
    public string PublicPlaceType { get; set; }
    public int Number { get; set; }
    public string Complement { get; set; }
    public string State { get; set; }
    public string City { get; set; }
}