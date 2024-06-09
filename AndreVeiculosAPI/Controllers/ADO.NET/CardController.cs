using Microsoft.Data.SqlClient;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.ADO.NET;
public class CardController
{
    private readonly string _connectionString;

    public CardController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public Card GetCard(string cardNumber)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Card.Select
        };

        command.Parameters.AddWithValue("@cardNumber", cardNumber);

        using SqlDataReader reader = command.ExecuteReader();
        Card card = new();

        if (reader.Read())
        {
            card = new Card
            {
                CardNumber = reader.GetString(0),
                SecurityKey = reader.GetString(1),
                DueDate = reader.GetString(2),
                CardName = reader.GetString(3),
            };
        }

        return card;
    }

    public Card PostCard(Card card)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Card.Insert
        };

        command.Parameters.AddWithValue("@CardNumber", card.CardNumber);
        command.Parameters.AddWithValue("@SecurityKey", card.SecurityKey);
        command.Parameters.AddWithValue("@DueDate", card.DueDate);
        command.Parameters.AddWithValue("@CardName", card.CardName);

        return card;
    }
}