using Dapper;
using Microsoft.Data.SqlClient;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.Dapper;

public class PixController
{
    public readonly string _connectionString;
    public readonly PixTypeController _pixTypeController;

    public PixController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _pixTypeController = new PixTypeController(configuration);
    }

    public Pix GetPix(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var row = connection.QuerySingle<dynamic>(Pix.Select, new { pixId = id });

        connection.Close();

        return new Pix
        {
            Id = row.Id,
            PixKey = row.PixKey,
            Type = _pixTypeController.GetPixType(row.TypeId)
        };
    }

    public Pix PostPix(Pix pix)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        _pixTypeController.PostPixType(pix.Type);
        pix.Id = (int)connection.ExecuteScalar(Pix.Insert, new { TypeId = pix.Type.Id, PixKey = pix.PixKey });


        connection.Close();

        return pix;
    }
}