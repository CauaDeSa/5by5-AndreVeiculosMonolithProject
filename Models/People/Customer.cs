using Models.PeopleDTO;

namespace Models.People;

public class Customer : Person
{
    public double Income { get; set; }
    public string DocumentPDF { get; set; }

    public Customer() { }

    public Customer(CustomerDTO customerDTO)
    {
        Document = customerDTO.Document;
        Name = customerDTO.Name;
        BirthDate = customerDTO.BirthDate;
        Address = new(){ Id = customerDTO.AddressId };
        Telephone = customerDTO.Telephone;
        Email = customerDTO.Email;
        Income = customerDTO.Income;
        DocumentPDF = customerDTO.DocumentPDF;
    }
}