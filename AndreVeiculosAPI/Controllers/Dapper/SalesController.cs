using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Profitable;
using Models.ProfitableDTO;

namespace AndreVeiculosAPI.Controllers.Dapper;

[ApiController]
[Route("api/[controller]")]
public class SalesController
{
    public readonly string _connectionString;
    public readonly CarsController _carController;
    public readonly CustomersController _customerController;
    public readonly EmployeesController _employeeController;
    public readonly PaymentsController _paymentController;

    public SalesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _carController = new(configuration);
        _customerController = new(configuration);
        _employeeController = new(configuration);
        _paymentController = new(configuration);
    }

    [HttpGet("dapper")]
    public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var row = connection.Query<dynamic>(Sale.SelectAll).ToList();

        var sales = new List<Sale>();

        foreach (var item in row)
        {
            sales.Add(new Sale
            {
                Id = item.Id,
                Car = (await _carController.GetCar(item.CarPlate)).Value,
                Customer = (await _customerController.GetCustomer(item.CustomerDocument)).Value,
                Employee = (await _employeeController.GetEmployee(item.EmployeeDocument)).Value,
                Payment = (await _paymentController.GetPayment(item.PaymentId)).Value,
                SaleDate = item.SaleDate
            });
        }

        connection.Close();

        return sales;
    }

    [HttpGet("dapper/{id}")]
    public async Task<ActionResult<Sale>> GetSale(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var row = connection.QuerySingle<dynamic>(Sale.Select, new { Id = id });

        connection.Close();

        return new Sale()
        {
            Id = row.Id,
            Car = (await _carController.GetCar(row.CarPlate)).Value,
            Customer = (await _customerController.GetCustomer(row.CustomerDocument)).Value,
            SalePrice = row.SalePrice,
            Employee = (await _employeeController.GetEmployee(row.EmployeeDocument)).Value,
            Payment = (await _paymentController.GetPayment(row.PaymentId)).Value,
            SaleDate = row.SaleDate
        };
    }

    [HttpPost("dapper")]
    public Task<ActionResult<Sale>> PostSale(SaleDTO dto)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        int id = connection.ExecuteScalar<int>(Sale.Insert, new{ CarPlate = dto.CarPlate, SaleDate = dto.SaleDate, SalePrice = dto.SalePrice, CustomerDocument = dto.CustomerDocument, EmployeeDocument = dto.EmployeeDocument, PaymentId = dto.PaymentId });

        connection.Close();

        return GetSale(id);
    }
}