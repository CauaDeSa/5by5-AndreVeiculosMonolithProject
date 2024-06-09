using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.People;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
public class EmployeesController
{
    private readonly string _connectionString;
    private readonly JobTitlesController _jobTitlesController;
    private readonly AddressController _addressController;

    public EmployeesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _jobTitlesController = new JobTitlesController(configuration);
        _addressController = new AddressController(configuration);
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Employee"
        };

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<Employee> employees = new();

        while (await reader.ReadAsync())
        {
            employees.Add(new Employee
            {
                Document = reader.GetString(0),
                Function = ( await _jobTitlesController.GetJobTitle(reader.GetInt32(1))).Value,
                ComissionAmount = reader.GetDouble(2),
                Comission = reader.GetDouble(3),
                Name = reader.GetString(4),
                BirthDate = reader.GetDateTime(5),
                Address = ( await _addressController.GetAddress(reader.GetInt32(6))).Value,
                Telephone = reader.GetString(7),
                Email = reader.GetString(8)
            });
        }

        return employees;
    }

    [HttpGet("ado/{document}")]
    public async Task<ActionResult<Employee>> GetEmployee(string document)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Employee WHERE Document = @document"
        };

        command.Parameters.AddWithValue("@Document", document);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        Employee employee = new();

        if (reader.Read())
        {
            employee = new Employee
            {
                Document = reader.GetString(0),
                Function = (await _jobTitlesController.GetJobTitle(reader.GetInt32(1))).Value,
                ComissionAmount = reader.GetDouble(2),
                Comission = reader.GetDouble(3),
                Name = reader.GetString(4),
                BirthDate = reader.GetDateTime(5),
                Address = (await _addressController.GetAddress(reader.GetInt32(6))).Value,
                Telephone = reader.GetString(7),
                Email = reader.GetString(8)
            };
        }

        return employee;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "INSERT INTO Employee (Document, FunctionId, ComissionAmount, Comission, Name, BirthDate, AddressId, Telephone, Email) OUTPUT INSERTED.Document VALUES (@Document, @FunctionId, @ComissionAmount, @Comission, @Name, @BirthDate, @AddressId, @Telephone, @Email)"
        };

        command.Parameters.AddWithValue("@Document", employee.Document);
        command.Parameters.AddWithValue("@FunctionId", employee.Function.Id);
        command.Parameters.AddWithValue("@ComissionAmount", employee.ComissionAmount);
        command.Parameters.AddWithValue("@Comission", employee.Comission);
        command.Parameters.AddWithValue("@Name", employee.Name);
        command.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
        command.Parameters.AddWithValue("@AddressId", employee.Address.Id);
        command.Parameters.AddWithValue("@Telephone", employee.Telephone);
        command.Parameters.AddWithValue("@Email", employee.Email);

        return await GetEmployee((string)await command.ExecuteScalarAsync());
    }
}
