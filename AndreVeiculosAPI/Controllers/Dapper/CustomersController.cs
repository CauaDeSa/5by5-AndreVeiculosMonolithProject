using AndreVeiculosAPI.Controllers.ADO.NET;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.People;
using Models.PeopleDTO;

namespace AndreVeiculosAPI.Controllers.Dapper;

[Route("api/[controller]")]
public class CustomersController
{
    private readonly string _connectionString;
    private readonly AddressesController _addressController;

    public CustomersController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _addressController = new AddressesController(configuration);
    }

    [HttpGet("dapper")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var dtos = await connection.QueryAsync<CustomerDTO>(Customer.SelectAll);

        connection.Close();

        var customers = new List<Customer>();

        foreach (var dto in dtos)
        {
            customers.Add(new Customer(dto) { Address = _addressController.GetAddress(dto.AddressId) });
        }

        return customers.ToList();
    }

    [HttpGet("dapper/{document}")]
    public async Task<ActionResult<Customer>> GetCustomer(string document)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var customer = await connection.QueryFirstOrDefaultAsync<Customer>(Customer.Select, new { Document = document });

        connection.Close();


        return customer;
    }

    [HttpPost("dapper")]
    public Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        connection.Execute(Customer.Insert, customer);

        connection.Close();

        return GetCustomer(customer.Document);
    }
}