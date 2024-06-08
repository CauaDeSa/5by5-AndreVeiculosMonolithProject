using System.ComponentModel.DataAnnotations;

namespace Models.People;

public abstract class Person
{
    [Key]
    public string Document { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public Address Address { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
}