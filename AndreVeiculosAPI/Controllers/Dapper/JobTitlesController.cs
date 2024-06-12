using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.People;

namespace AndreVeiculosAPI.Controllers.Dapper;

public class JobTitlesController : ControllerBase
{
    public readonly string _connectionString;

    public JobTitlesController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AndreVeiculosAPIContext");
    }

    public async Task<ActionResult<IEnumerable<JobTitle>>> GetJobTitles()
    {
        using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        var jobTitles = connection.Query<JobTitle>(JobTitle.SelectAll).ToList();

        connection.Close();

        return jobTitles;
    }

    public JobTitle GetJobTitle(int id)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        var jobTitle = connection.QuerySingle<JobTitle>(JobTitle.Select, new { Id = id });

        connection.Close();

        return jobTitle;
    }

    public Task<ActionResult<JobTitle>> PostJobTitle(JobTitle jobTitle)
    {
        using SqlConnection connection = new(_connectionString);
        connection.Open();

        jobTitle.Id = (int)connection.ExecuteScalar(JobTitle.Insert, jobTitle);

        connection.Close();

        return Task.FromResult<ActionResult<JobTitle>>(jobTitle);
    }
}