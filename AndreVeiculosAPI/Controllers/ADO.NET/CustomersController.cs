using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.People;
using Models.PeopleDTO;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
public class CustomersController
{
    private readonly string _connectionString;
    private readonly AddressController _addressController;

    public CustomersController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _addressController = new AddressController(configuration);
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Customer.SelectAll
        };

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<CustomerDTO> dtos = new();
        List<Customer> customers = new();

        while (await reader.ReadAsync())
        {
            dtos.Add(new CustomerDTO
            {
                Document = reader.GetString(0),
                Income = reader.GetDouble(1),
                DocumentPDF = reader.GetString(2),
                Name = reader.GetString(3),
                BirthDate = reader.GetDateTime(4),
                AddressId = reader.GetInt32(5),
                Telephone = reader.GetString(6),
                Email = reader.GetString(7),
            });
        }

        foreach (var dto in dtos)
        {
            customers.Add(new Customer
            {
                Document = dto.Document,
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Address = (await _addressController.GetAddress(dto.AddressId)).Value,
                Telephone = dto.Telephone,
                Email = dto.Email,
                Income = dto.Income,
                DocumentPDF = dto.DocumentPDF
            });
        }

        return customers;
    }

    [HttpGet("ado/{document}")]
    public async Task<ActionResult<Customer>> GetCustomer(string document)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Customer.Select
        };

        command.Parameters.AddWithValue("@Document", document);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        Customer customer = new();

        if (reader.Read())
        {
            customer = new Customer
            {
                Document = reader.GetString(0),
                Income = reader.GetDouble(1),
                DocumentPDF = reader.GetString(2),
                Name = reader.GetString(3),
                BirthDate = reader.GetDateTime(4),
                Address = (await _addressController.GetAddress(reader.GetInt32(5))).Value,
                Telephone = reader.GetString(6),
                Email = reader.GetString(7),
            };
        }

        return customer;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Customer.Insert
        };

        command.Parameters.AddWithValue("@Document", customer.Document);
        command.Parameters.AddWithValue("@Income", customer.Income);
        command.Parameters.AddWithValue("@DocumentPDF", customer.DocumentPDF);
        command.Parameters.AddWithValue("@Name", customer.Name);
        command.Parameters.AddWithValue("@BirthDate", customer.BirthDate);
        command.Parameters.AddWithValue("@AddressId", _addressController.PostAddress(customer.Address));
        command.Parameters.AddWithValue("@Telephone", customer.Telephone);
        command.Parameters.AddWithValue("@Email", customer.Email);

        return await GetCustomer((string)await command.ExecuteScalarAsync());
    }
}