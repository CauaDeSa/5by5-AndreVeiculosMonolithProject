using Models.PeopleDTO;

namespace Models.People;

public class Employee : Person
{
    public static readonly string SelectAll = "SELECT * FROM Employee";
    public static readonly string Select = "SELECT * FROM Employee WHERE Document = @document";
    public static readonly string Insert = "INSERT INTO Employee (Document, FunctionId, ComissionAmount, Comission, Name, BirthDate, AddressId, Telephone, Email) OUTPUT INSERTED.Document VALUES (@Document, @FunctionId, @ComissionAmount, @Comission, @Name, @BirthDate, @AddressId, @Telephone, @Email)";

    public JobTitle Function { get; set; }
    public double ComissionAmount { get; set; }
    public double Comission { get; set; }

    public Employee() { }

    public Employee(EmployeeDTO employeeDTO)
    {
        Document = employeeDTO.Document;
        Name = employeeDTO.Name;
        BirthDate = employeeDTO.BirthDate;
        Address = employeeDTO.Address;
        Telephone = employeeDTO.Telephone;
        Email = employeeDTO.Email;
        Function = new() { Id = employeeDTO.FunctionId };
        ComissionAmount = employeeDTO.ComissionAmount;
        Comission = employeeDTO.Comission;
    }
}