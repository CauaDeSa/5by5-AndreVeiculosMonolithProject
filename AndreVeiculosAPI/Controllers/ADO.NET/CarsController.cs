using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Cars;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
public class CarsController
{
    private readonly string _connectionString;

    public CarsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<Car>>> GetCar()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Car.SelectAll
        };

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<Car> cars = new();

        while (await reader.ReadAsync())
        {
            cars.Add(new Car
            {
                Plate = reader.GetString(0),
                Name = reader.GetString(1),
                ModelYear = reader.GetInt32(2),
                ManufactureYear = reader.GetInt32(3),
                Color = reader.GetString(4),
                Sold = reader.GetBoolean(5)
            });
        }

        return cars;
    }

    [HttpGet("ado/{plate}")]
    public async Task<ActionResult<Car>> GetCar(string plate)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Car.Select
        };

        command.Parameters.AddWithValue("@plate", plate);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        Car car = new();

        if (await reader.ReadAsync())
        {
            car = new Car
            {
                Plate = reader.GetString(0),
                Name = reader.GetString(1),
                ModelYear = reader.GetInt32(2),
                ManufactureYear = reader.GetInt32(3),
                Color = reader.GetString(4),
                Sold = reader.GetBoolean(5)
            };
        }

        return car;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<Car>> PostCar(Car car)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = Car.Insert
        };

        command.Parameters.AddWithValue("@plate", car.Plate);
        command.Parameters.AddWithValue("@name", car.Name);
        command.Parameters.AddWithValue("@modelYear", car.ModelYear);
        command.Parameters.AddWithValue("@manufactureYear", car.ManufactureYear);
        command.Parameters.AddWithValue("@color", car.Color);
        command.Parameters.AddWithValue("@sold", car.Sold);

        return await GetCar((string)await command.ExecuteScalarAsync());
    }   
}