using System.Reflection.Metadata;
using System;
using Models.PeopleDTO;

namespace Models.People;

public class Customer : Person
{
    public decimal Income { get; set; }
    public string DocumentPDF { get; set; }

    public Customer() { }

    public Customer(CustomerDTO customerDTO)
    {
        Document = customerDTO.Document;
        Name = customerDTO.Name;
        BirthDate = customerDTO.BirthDate;
        Address = customerDTO.Address;
        Telephone = customerDTO.Telephone;
        Email = customerDTO.Email;
        Income = customerDTO.Income;
        DocumentPDF = customerDTO.DocumentPDF;
    }
}