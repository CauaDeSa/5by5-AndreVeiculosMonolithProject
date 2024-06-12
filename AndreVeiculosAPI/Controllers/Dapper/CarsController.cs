using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Cars;

namespace AndreVeiculosAPI.Controllers.Dapper;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly string _connectionString;

    public CarsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    [HttpGet("dapper")]
    public async Task<ActionResult<IEnumerable<Car>>> GetCars()
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        var cars = await connection.QueryAsync<Car>(Car.SelectAll);

        return cars.ToList();
    }

    [HttpGet("dapper/{plate}")]
    public async Task<ActionResult<Car>> GetCar(string plate)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        var car = await connection.QueryFirstOrDefaultAsync<Car>(Car.Select, new { Plate = plate });

        return car;
    }

    [HttpPost("dapper")]
    public Task<ActionResult<Car>> PostCar(Car car)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        connection.Execute(Car.Insert, car);

        connection.Close();

        return GetCar(car.Plate);
    }
}