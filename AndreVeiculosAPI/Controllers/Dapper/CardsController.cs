using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.Dapper;

public class CardController
{
    private readonly string _connectionString;

    public CardController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public async Task<ActionResult<Card>> GetCard(int id)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var card = connection.QuerySingle<Card>(Card.Select, new { cardNumber = id });

        connection.Close();

        return card;
    }

    public int PostCard(Card card)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        int id = connection.ExecuteScalar<int>(Card.Insert, new { card.CardNumber, card.SecurityKey, card.DueDate, card.CardName });

        connection.Close();

        return id;
    }
}