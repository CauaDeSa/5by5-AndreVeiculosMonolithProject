using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Profitable;
using Models.ProfitableDTO;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
public class PaymentsController
{
    private readonly string _connectionString;
    private readonly CardController _cardController;
    private readonly FetlockController _fetlockController;
    private readonly PixController _pixController;

    public PaymentsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _cardController = new CardController(configuration);
        _fetlockController = new FetlockController(configuration);
        _pixController = new PixController(configuration);
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Payment"
        };

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<Payment> payments = new();

        while (await reader.ReadAsync())
        {
            payments.Add(new Payment
            {
                Id = reader.GetInt32(0),
                Card = _cardController.GetCard(reader.GetString(1)),
                Fetlock = _fetlockController.GetFetlock(reader.GetInt32(2)),
                Pix = _pixController.GetPix(reader.GetInt32(3)),
                PaymentDate = reader.GetDateTime(4),

            });
        }

        return payments;
    }

    [HttpGet("ado/{id}")]
    public async Task<ActionResult<Payment>> GetPayment(int id)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Payment WHERE Id = @id"
        };

        command.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        Payment payment = new();

        if (await reader.ReadAsync())
        {
            payment = new Payment
            {
                Id = reader.GetInt32(0),
                Card = _cardController.GetCard(reader.GetString(1)),
                Fetlock = _fetlockController.GetFetlock(reader.GetInt32(2)),
                Pix = _pixController.GetPix(reader.GetInt32(3)),
                PaymentDate = reader.GetDateTime(4),
            };
        }

        return payment;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<Payment>> PostPayment(PaymentDTO dto)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "INSERT INTO Payment (CardNumber, FetlockId, PixId, PaymentDate) OUTPUT INSERTED.Id VALUES (@CardNumber, @FetlockId, @PixId, @PaymentDate)"
        };

        command.Parameters.AddWithValue("@CardNumber", _cardController.GetCard(dto.CardNumber).CardNumber);
        command.Parameters.AddWithValue("@FetlockId", _fetlockController.GetFetlock(dto.FetlockId).Id);
        Console.WriteLine("Passou");
        command.Parameters.AddWithValue("@PixId", _pixController.GetPix(dto.PixId).Id);
        command.Parameters.AddWithValue("@PaymentDate", dto.PaymentDate);

        return await GetPayment((int)command.ExecuteScalar());
    }
}