using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.People;

namespace AndreVeiculosAPI.Controllers.Dapper;

public class AddressesController : ControllerBase
{
    private readonly string _connectionString;

    public AddressesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public Address GetAddress(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var address = connection.QuerySingle<Address>(Address.Select, new { Id = id });

        connection.Close();

        return address;
    }

    public Address PostAddress(Address address)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        address.Id = connection.QuerySingle<int>(Address.Insert, address);

        connection.Close();

        return address;
    }
}