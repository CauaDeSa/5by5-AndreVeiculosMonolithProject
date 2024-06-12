using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Profitable;
using Models.ProfitableDTO;
using System.ComponentModel.DataAnnotations;

namespace AndreVeiculosAPI.Controllers.Dapper;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController
{
    public readonly string _connectionString;
    public readonly CardController _cardController;
    public readonly PixController _pixController;
    public readonly FetlockController _fetlockController;

    public PaymentsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _cardController = new(configuration);
        _pixController = new(configuration);
        _fetlockController = new(configuration);
    }

    [HttpGet("dapper")]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var row = connection.Query<dynamic>(Payment.SelectAll).ToList();

        List<Payment> payments = new();

        foreach (var item in row)
        {
            payments.Add(new Payment
            {
                Id = item.Id,
                Card = _cardController.GetCard(item.CardNumber),
                Fetlock = _fetlockController.GetFetlock(item.FetlockId),
                Pix = _pixController.GetPix(item.PixId),
                PaymentDate = item.PaymentDate
            });
        }

        connection.Close();

        return payments;
    }

    [HttpGet("dapper/{id}")]
    public async Task<ActionResult<Payment>> GetPayment(int id)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var row = connection.QueryFirstOrDefault<dynamic>(Payment.Select, new { Id = id });

        connection.Close();

        return new Payment
        {
            Id = row.Id,
            Card = _cardController.GetCard(row.CardNumber),
            Fetlock = _fetlockController.GetFetlock(row.FetlockId),
            Pix = _pixController.GetPix(row.PixId),
            PaymentDate = row.PaymentDate
        };
    }

    [HttpPost("dapper")]
    public Task<ActionResult<Payment>> PostPayment(Payment payment)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        _cardController.PostCard(payment.Card);
        _fetlockController.PostFetlock(payment.Fetlock);
        _pixController.PostPix(payment.Pix);

        payment.Id = (int)connection.ExecuteScalar(Payment.Insert, new{ CardNumber = payment.Card.CardNumber, FetlockId = payment.Fetlock.Id, PixId = payment.Pix.Id, PaymentDate = payment.PaymentDate});

        connection.Close();

        return Task.FromResult<ActionResult<Payment>>(payment);
    }
}