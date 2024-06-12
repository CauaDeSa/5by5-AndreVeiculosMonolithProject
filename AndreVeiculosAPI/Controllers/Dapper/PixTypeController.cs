using Dapper;
using Microsoft.Data.SqlClient;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.Dapper;

public class PixTypeController
{
    public readonly string _connectionString;

    public PixTypeController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public PixType GetPixType(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var pixType = connection.QuerySingle<PixType>(PixType.Select, new { pixTypeId = id });

        connection.Close();

        return pixType;
    }

    public PixType PostPixType(PixType pixType)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        pixType.Id = (int)connection.ExecuteScalar(PixType.Insert, new { pixType.Name });

        connection.Close();

        return pixType;
    }
}