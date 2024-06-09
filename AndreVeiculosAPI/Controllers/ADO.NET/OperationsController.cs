using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.Cars;
using Models.CarsDTO;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
public class OperationsController
{
    private readonly string _connectionString;

    public OperationsController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<Operation>>> GetOperation()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Operation"
        };

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<Operation> operations = new();

        while (await reader.ReadAsync())
        {
            operations.Add(new Operation
            {
                Id = reader.GetInt32(0),
                Description = reader.GetString(1),
            });
        }

        return operations;
    }

    [HttpGet("ado/{id}")]
    public async Task<ActionResult<Operation>> GetOperation(int id)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM Operation WHERE Id = @id"
        };

        command.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        Operation operation = new();

        if (reader.Read())
        {
            operation = new Operation
            {
                Id = reader.GetInt32(0),
                Description = reader.GetString(1),
            };
        }

        return operation;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<Operation>> PostOperation(OperationDTO operationDTO)
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "INSERT INTO Operation (Description) OUTPUT INSERTED.Id VALUES (@description)"
        };

        command.Parameters.AddWithValue("@description", operationDTO.Description);

        return await GetOperation((int)await command.ExecuteScalarAsync());
    }
}