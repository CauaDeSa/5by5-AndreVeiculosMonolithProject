using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Cars;

namespace AndreVeiculosAPI.Controllers.Dapper;

[Route("api/[controller]")]
public class OperationsController
{
    private readonly string _connectionString;

    public OperationsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    [HttpGet("dapper")]
    public async Task<ActionResult<IEnumerable<Operation>>> GetOperations()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var operations = await connection.QueryAsync<Operation>(Operation.SelectAll);

        connection.Close();

        return operations.ToList();
    }

    [HttpGet("dapper{id}")]
    public async Task<ActionResult<Operation>> GetOperation(int id)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var operation = await connection.QueryFirstOrDefaultAsync<Operation>(Operation.Select, new { Id = id });

        connection.Close();

        return operation;
    }

    [HttpPost("dapper")]
    public Operation PostOperation(Operation operation)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        operation.Id = connection.ExecuteScalar<int>(Operation.Insert, operation);

        connection.Close();

        return operation;
    }
}