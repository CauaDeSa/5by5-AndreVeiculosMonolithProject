using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.People;
using Models.PeopleDTO;

namespace AndreVeiculosAPI.Controllers.ADO.NET;

[Route("api/[controller]")]
public class JobTitlesController
{
    private readonly string _connectionString;

    public JobTitlesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    [HttpGet("ado")]
    public async Task<ActionResult<IEnumerable<JobTitle>>> GetJobTitle()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = "SELECT * FROM JobTitle"
        };

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        List<JobTitle> jobTitles = new();

        while (await reader.ReadAsync())
        {
            jobTitles.Add(new JobTitle
            {
                Id = reader.GetInt32(0),
                Description = reader.GetString(1),
            });
        }

        return jobTitles;
    }

    [HttpGet("ado/{id}")]
    public async Task<ActionResult<JobTitle>> GetJobTitle(int id)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();

        SqlCommand command = new()
        {
            Connection = connection,
            CommandText = JobTitle.Select
        };

        command.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = await command.ExecuteReaderAsync();
        JobTitle jobTitle = new();

        if (reader.Read())
        {
            jobTitle = new JobTitle
            {
                Id = reader.GetInt32(0),
                Description = reader.GetString(1),
            };
        }

        return jobTitle;
    }

    [HttpPost("ado")]
    public async Task<ActionResult<JobTitle>> PostJobTitle(JobTitleDTO dto)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        using SqlCommand command = new()
        {
            Connection = connection,
            CommandText = JobTitle.Insert
        };

        command.Parameters.AddWithValue("@description", dto.Description);

        return await GetJobTitle((int)command.ExecuteScalar());
    }
}