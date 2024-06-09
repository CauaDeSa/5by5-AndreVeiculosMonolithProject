using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.People;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

public class AddressController
{
    private readonly string _connectionString;

    public AddressController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public async Task<ActionResult<Address>> GetAddress(int id)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Address.Select
        };

        command.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        Address address = new();

        if (reader.Read())
        {
            address = new Address
            {
                Id = reader.GetInt32(0),
                PublicPlace = reader.GetString(1),
                ZipCode = reader.GetString(2),
                Neighborhood = reader.GetString(3),
                PublicPlaceType = reader.GetString(4),
                Number = reader.GetInt32(5),
                Complement = reader.GetString(6),
                State = reader.GetString(7),
                City = reader.GetString(8)
            };
        }

        return address;
    }

    public int PostAddress(Address address)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Address.Insert
        };

        command.Parameters.AddWithValue("@publicPlace", address.PublicPlace);
        command.Parameters.AddWithValue("@zipCode", address.ZipCode);
        command.Parameters.AddWithValue("@neighborhood", address.Neighborhood);
        command.Parameters.AddWithValue("@publicPlaceType", address.PublicPlaceType);
        command.Parameters.AddWithValue("@number", address.Number);
        command.Parameters.AddWithValue("@complement", address.Complement);
        command.Parameters.AddWithValue("@state", address.State);
        command.Parameters.AddWithValue("@city", address.City);

        return (int)command.ExecuteScalar();
    }
}