using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Profitable;
using Models.ProfitableDTO;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
public class PurchasesController
{
    private readonly string _connectionString;
    private readonly CarsController _carController;

    public PurchasesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _carController = new(configuration);
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchase()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Purchase"
        };

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<Purchase> purchases = new();

        while (await reader.ReadAsync())
        {
            purchases.Add(new Purchase
            {
                Id = reader.GetInt32(0),
                Car = (await _carController.GetCar(reader.GetString(1))).Value,
                Price = reader.GetDouble(2),
                PurchaseDate = reader.GetDateTime(3)
            });
        }

        return purchases;
    }

    [HttpGet("ado/{id}")]
    public async Task<ActionResult<Purchase>> GetPurchase(int id)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Purchase WHERE Id = @id"
        };

        command.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        Purchase purchase = new();

        if (await reader.ReadAsync())
        {
            purchase = new Purchase
            {
                Id = reader.GetInt32(0),
                Car = (await _carController.GetCar(reader.GetString(1))).Value,
                Price = reader.GetDouble(2),
                PurchaseDate = reader.GetDateTime(3)
            };
        }

        return purchase;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<Purchase>> PostPurchase(PurchaseDTO dto)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "INSERT INTO Purchase (CarPlate, Price, PurchaseDate) OUTPUT INSERTED.Id VALUES (@carPlate, @price, @purchaseDate)"
        };

        command.Parameters.AddWithValue("@carPlate", dto.CarPlate);
        command.Parameters.AddWithValue("@price", dto.Price);
        command.Parameters.AddWithValue("@purchaseDate", dto.PurchaseDate);

        return await GetPurchase((int)await command.ExecuteScalarAsync());
    }
}