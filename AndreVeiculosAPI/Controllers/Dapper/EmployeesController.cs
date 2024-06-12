using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.People;
using Models.PeopleDTO;

namespace AndreVeiculosAPI.Controllers.Dapper;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly string _connectionString;
    private readonly AddressesController _addressController;
    private readonly JobTitlesController _jobTitleController;

    public EmployeesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _addressController = new(configuration);
        _jobTitleController = new(configuration);
    }

    [HttpGet("dapper")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var dtos = connection.Query<EmployeeDTO>(Employee.SelectAll).ToList();
        List<Employee> employees = new();

        foreach (var dto in dtos)
            employees.Add(CreateEmployee(dto));

        connection.Close();

        return employees;
    }

    [HttpGet("dapper/{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(string document)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var dto = connection.QuerySingle<EmployeeDTO>(Employee.Select, new { Document = document });

        if (dto is null)
        {
            return NotFound();
        }

        var employee = CreateEmployee(dto);

        connection.Close();

        return employee;
    }

    private Employee CreateEmployee(EmployeeDTO dto)
    {
        return new(dto)
        {
            Address = _addressController.GetAddress(dto.AddressId),
            Function = _jobTitleController.GetJobTitle(dto.FunctionId)
        };
    }

    [HttpPost("dapper")]
    public Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        
        employee.Address = _addressController.PostAddress(employee.Address);
        _jobTitleController.PostJobTitle(employee.Function);

        connection.Execute(Employee.Insert, new { Document = employee.Document, FunctionId = employee.Function.Id, ComissionAmount = employee.ComissionAmount, Comission = employee.Comission, Name = employee.Name, BirthDate = employee.BirthDate, AddressId = employee.Address.Id, Telephone = employee.Address.Id, Email = employee.Email});


        connection.Close();

        return GetEmployee(employee.Document);
    }
}