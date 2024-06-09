using Microsoft.Data.SqlClient;
using Models.Profitable;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

public class PixController
{
    private readonly string _connectionString;
    private readonly PixTypeController _pixTypeController;

    public PixController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _pixTypeController = new PixTypeController(configuration);
    }

    public Pix GetPix(int pixId)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Pix WHERE Id = @pixId"
        };

        command.Parameters.AddWithValue("@pixId", pixId);

        using SqlDataReader reader = command.ExecuteReader();
        Pix pix = new();

        if (reader.Read())
        {
            pix = new Pix
            {
                Id = reader.GetInt32(0),
                Type = _pixTypeController.GetPixType(reader.GetInt32(1)),
                PixKey = reader.GetString(2),
            };
        }

        return pix;
    }

    public Pix PostPix(Pix pix)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "INSERT INTO Pix (TypeId, PixKey) OUTPUT INSERTED.Id VALUES (@TypeId, @PixKey)"
        };

        command.Parameters.AddWithValue("@TypeId", _pixTypeController.GetPixType(pix.Type.Id).Id);
        command.Parameters.AddWithValue("@PixKey", pix.PixKey);
        pix.Id = (int)command.ExecuteScalar();


        return pix;
    }
}