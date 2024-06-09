using Microsoft.Data.SqlClient;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

public class PixTypeController
{
    private readonly string _connectionString;

    public PixTypeController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public PixType GetPixType(int pixTypeId)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = PixType.Select
        };

        command.Parameters.AddWithValue("@pixTypeId", pixTypeId);

        using SqlDataReader reader = command.ExecuteReader();
        PixType pixType = new();

        if (reader.Read())
        {
            pixType = new PixType
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
            };
        }

        return pixType;
    }

    public PixType PostPixType(PixType pixType)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = PixType.Insert
        };

        command.Parameters.AddWithValue("@Name", pixType.Name);

        return GetPixType((int)command.ExecuteScalar());
    }
}