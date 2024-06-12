using Dapper;
using Microsoft.Data.SqlClient;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.Dapper;

public class FetlockController
{
    public readonly string _connectionString;

    public FetlockController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public Fetlock GetFetlock(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var fetlock = connection.QuerySingle<Fetlock>(Fetlock.Select, new { fetlockId = id });

        connection.Close();

        return fetlock;
    }

    public Fetlock PostFetlock(Fetlock fetlock)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        fetlock.Id = (int)connection.ExecuteScalar(Fetlock.Insert, new { Number = fetlock.Number, DueDate = fetlock.DueDate });

        connection.Close();

        return fetlock;
    }
}