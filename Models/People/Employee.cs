using Models.PeopleDTO;

namespace Models.People;

public class Employee : Person
{
    public JobTitle Function { get; set; }
    public Decimal ComissionAmount { get; set; }
    public Decimal Comission { get; set; }

    public Employee() { }

    public Employee(EmployeeDTO employeeDTO)
    {
        Document = employeeDTO.Document;
        Name = employeeDTO.Name;
        BirthDate = employeeDTO.BirthDate;
        Address = employeeDTO.Address;
        Telephone = employeeDTO.Telephone;
        Email = employeeDTO.Email;
        Function = employeeDTO.Function;
        ComissionAmount = employeeDTO.ComissionAmount;
        Comission = employeeDTO.Comission;
    }
}