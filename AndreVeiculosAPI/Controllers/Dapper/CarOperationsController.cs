using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Cars;
using Dapper;
using Models.CarsDTO;

namespace AndreVeiculosAPI.Controllers.Dapper;

[Route("api/[controller]")]
[ApiController]
public class CarOperationsController : ControllerBase
{
    private readonly string _connectionString;
    private readonly CarsController _carsController;
    private readonly OperationsController _operationController;

    public CarOperationsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
        _carsController = new CarsController(configuration);
        _operationController = new OperationsController(configuration);
    }

    [HttpGet("dapper")]
    public async Task<ActionResult<IEnumerable<CarOperation>>> GetCarOperation()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        List<CarOperationDTO> dtos = connection.Query<CarOperationDTO>(CarOperation.SelectAll).ToList();
        List<CarOperation> carOperations = connection.Query<CarOperation>(CarOperation.SelectAll).ToList();

        connection.Close();

        for (int i = 0; i < carOperations.Count; i++)
        {
            carOperations[i].Car = (await _carsController.GetCar(dtos[i].CarPlate)).Value;
            carOperations[i].Operation = (await _operationController.GetOperation(dtos[i].OperationId)).Value;
        }

        return carOperations;
    }

    [HttpGet("dapper{id}")]
    public async Task<ActionResult<CarOperation>> GetCarOperation(int id)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var carOperation = connection.QuerySingle<CarOperation>(CarOperation.Select, new { id });
        var dto = connection.QuerySingle<CarOperationDTO>(CarOperation.Select, new { id });

        connection.Close();

        carOperation.Car = (await _carsController.GetCar(dto.CarPlate)).Value;
        carOperation.Operation = (await _operationController.GetOperation(dto.OperationId)).Value;

        return carOperation;
    }

    [HttpPost("dapper")]
    public Task<ActionResult<CarOperation>> PostCarOperation(CarOperationDTO dto)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        int id = connection.ExecuteScalar<int>(CarOperation.Insert, dto);

        connection.Close();

        return GetCarOperation(id);
    }
}