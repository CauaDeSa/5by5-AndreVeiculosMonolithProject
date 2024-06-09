using Microsoft.Data.SqlClient;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

public class FetlockController
{
    private readonly string _connectionString;

    public FetlockController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public Fetlock GetFetlock(int fetlockId)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Fetlock WHERE Id = @fetlockId"
        };

        command.Parameters.AddWithValue("@fetlockId", fetlockId);

        using SqlDataReader reader = command.ExecuteReader();
        Fetlock fetlock = new();

        if (reader.Read())
        {
            fetlock = new Fetlock
            {
                Id = reader.GetInt32(0),
                Number = reader.GetInt32(1),
                DueDate = reader.GetDateTime(2),
            };
        }

        return fetlock;
    }

    public Fetlock PostFetlock(Fetlock fetlock)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "INSERT INTO Fetlock (Number, DueDate) OUPTPUT INSERTED.Id VALUES (@Number, @DueDate)"
        };

        command.Parameters.AddWithValue("@Number", fetlock.Number);
        command.Parameters.AddWithValue("@DueDate", fetlock.DueDate);

        return GetFetlock((int)command.ExecuteScalar());
    }
}