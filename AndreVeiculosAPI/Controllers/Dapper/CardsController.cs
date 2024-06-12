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

    public Card GetCard(string cardNumber)
    {
        using SqlConnection connection = new(_connectionString);
        connection.OpenAsync();

        var card = connection.QuerySingle<Card>(Card.Select, new { cardNumber = cardNumber });

        connection.Close();

        return card;
    }

    public void PostCard(Card card)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        if (GetCard(card.CardNumber) != null)
        {
            return;
        }
        connection.ExecuteScalar<int>(Card.Insert, new { card.CardNumber, card.SecurityKey, card.DueDate, card.CardName });

        connection.Close();
    }
}