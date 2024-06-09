using Models.PeopleDTO;

namespace Models.People;

public class Customer : Person
{
    public static readonly string SelectAll = "SELECT * FROM Customer";
    public static readonly string Select = "SELECT * FROM Customer WHERE Document = @document";
    public static readonly string Insert = "INSERT INTO Customer (Document, Income, DocumentPDF, Name, BirthDate, AddressId, Telephone, Email) OUTPUT INSERTED.Document VALUES (@Document, @Income, @DocumentPDF, @Name, @BirthDate, @AddressId, @Telephone, @Email)";

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