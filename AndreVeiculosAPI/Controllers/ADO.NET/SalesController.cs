using AndreVeiculosAPI.Controllers.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Cars;
using Models.Profitable;
using Models.ProfitableDTO;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
public class SalesController
{
    private readonly string _connectionString;
    private readonly CarsController _carController;
    private readonly CustomersController _customersController;
    private readonly EmployeesController _employeesController;
    private readonly PaymentsController _paymentsController;

    public SalesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _carController = new(configuration);
        _customersController = new(configuration);
        _employeesController = new(configuration);
        _paymentsController = new(configuration);
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<Sale>>> GetSale()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Sale"
        };

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<Sale> sales = new();

        while (await reader.ReadAsync())
        {
            sales.Add(new Sale()
            {
                Id = reader.GetInt32(0),
                Car = (await _carController.GetCar(reader.GetString(1))).Value,
                SaleDate = reader.GetDateTime(2),
                SalePrice = reader.GetDouble(3),
                Customer = (await _customersController.GetCustomer(reader.GetString(4))).Value,
                Employee = (await _employeesController.GetEmployee(reader.GetString(5))).Value,
                Payment = (await _paymentsController.GetPayment(reader.GetInt32(6))).Value
            });
        }

        return sales;
    }

    [HttpGet("ado/{id}")]
    public async Task<ActionResult<Sale>> GetSale(int id)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Sale WHERE Id = @id"
        };

        command.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        Sale sale = new();

        if (reader.Read())
        {
            sale = new Sale
            {
                Id = reader.GetInt32(0),
                Car = (await _carController.GetCar(reader.GetString(1))).Value,
                SaleDate = reader.GetDateTime(2),
                SalePrice = reader.GetDouble(3),
                Customer = (await _customersController.GetCustomer(reader.GetString(4))).Value,
                Employee = (await _employeesController.GetEmployee(reader.GetString(5))).Value,
                Payment = (await _paymentsController.GetPayment(reader.GetInt32(6))).Value
            };
        }

        return sale;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<Sale>> PostSale(SaleDTO dto)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "INSERT INTO Sale (CarPlate, SaleDate, SalePrice, CustomerDocument, EmployeeDocument, PaymentId) OUTPUT INSERTED.Id VALUES (@carPlate, @saleDate, @salePrice, @customerDocument, @employeeDocument, @paymentId)"
        };

        command.Parameters.AddWithValue("@carPlate", dto.CarPlate);
        command.Parameters.AddWithValue("@saleDate", dto.SaleDate);
        command.Parameters.AddWithValue("@salePrice", dto.SalePrice);
        command.Parameters.AddWithValue("@customerDocument", dto.CustomerDocument);
        command.Parameters.AddWithValue("@employeeDocument", dto.EmployeeDocument);
        command.Parameters.AddWithValue("@paymentId", dto.PaymentId);

        return await GetSale((int) await command.ExecuteScalarAsync());
    }
}