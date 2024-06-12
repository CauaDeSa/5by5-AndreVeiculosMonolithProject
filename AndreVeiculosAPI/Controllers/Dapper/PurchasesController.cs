using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Models.Profitable;
using Models.ProfitableDTO;

namespace AndreVeiculosAPI.Controllers.Dapper;

[ApiController]
[Route("api/[controller]")]
public class PurchasesController
{
    public readonly string _connectionString;
    public readonly CarsController _carController;

    public PurchasesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _carController = new(configuration);
    }

    [HttpGet("dapper")]
    public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var row = connection.Query<dynamic>(Purchase.SelectAll).ToList();

        var purchases = new List<Purchase>();

        foreach (var item in row)
        {
            purchases.Add(new Purchase
            {
                Id = item.Id,
                Car = (await _carController.GetCar(item.CarPlate)).Value,
                Price = item.Price,
                PurchaseDate = item.PurchaseDate
            });
        }

        connection.Close();

        return purchases;
    }

    [HttpGet("dapper/{id}")]
    public async Task<ActionResult<Purchase>> GetPurchase(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var row = connection.QuerySingle<dynamic>(Purchase.Select, new { Id = id });

        connection.Close();

        return new Purchase()
        {
            Id = row.Id,
            Car = (await _carController.GetCar(row.CarPlate)).Value,
            Price = row.Price,
            PurchaseDate = row.PurchaseDate
        };
    }

    [HttpPost("dapper")]
    public Task<ActionResult<Purchase>> PostPurchase(PurchaseDTO dto)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var id = (int)connection.ExecuteScalar(Purchase.Insert, new { carPlate = dto.CarPlate, Price = dto.Price, PurchaseDate = dto.PurchaseDate });

        connection.Close();

        return GetPurchase(id);
    }
}