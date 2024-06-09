using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Cars;
using Models.CarsDTO;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
[ApiController]
public class CarOperationsController
{
    private readonly string _connectionString;
    private readonly CarsController carController;
    private readonly OperationsController operationController;

    public CarOperationsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        carController = new CarsController(configuration);
        operationController = new OperationsController(configuration);
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<CarOperation>>> GetCarOperation()
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            CommandText = CarOperation.SelectAll,
            Connection = connection
        };

        SqlDataReader reader = command.ExecuteReader();
        List<CarOperation> carOperations = new();

        while (reader.Read())
        {
            carOperations.Add(new CarOperation
            {
                Id = reader.GetInt32(0),
                Car = new() { Plate = reader.GetString(1) },
                Operation = new() { Id = reader.GetInt32(2) }
            });
        }

        foreach (var carOperation in carOperations)
        {
            carOperation.Car = (await carController.GetCar(carOperation.Car.Plate)).Value;
            carOperation.Operation = (await operationController.GetOperation(carOperation.Operation.Id)).Value;
        }

        return carOperations;
    }

    [HttpGet("ado/{id}")]
    public async Task<ActionResult<CarOperation>> GetCarOperation(int id)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        SqlCommand command = new()
        {
            CommandText = CarOperation.Select,
            Connection = connection
        };

        command.Parameters.AddWithValue("@id", id);
        
        using SqlDataReader reader = await command.ExecuteReaderAsync();
        CarOperation carOperation = new();

        if (await reader.ReadAsync())
        {
            carOperation = new CarOperation
            {
                Id = reader.GetInt32(0),
                Car = new() { Plate = reader.GetString(1) },
                Operation = new() { Id = reader.GetInt32(2) }
            };

            carOperation.Car = (await carController.GetCar(carOperation.Car.Plate)).Value;
            carOperation.Operation = (await operationController.GetOperation(carOperation.Operation.Id)).Value;

            return carOperation;
        }

        return carOperation;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<CarOperation>> PostCarOperation(CarOperationDTO dto)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        SqlCommand command = new()
        {
            CommandText = CarOperation.Insert,
            Connection = connection
        };

        command.Parameters.AddWithValue("@carPlate", dto.CarPlate);
        command.Parameters.AddWithValue("@operationId", dto.OperationId);
        command.Parameters.AddWithValue("@operationStatus", dto.OperationStatus);

        return await GetCarOperation((int)await command.ExecuteScalarAsync());
    }   
}