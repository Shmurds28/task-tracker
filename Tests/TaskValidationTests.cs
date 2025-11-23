using System.Net;
using System.Net.Http.Json;
using Application.DTO;
using Xunit;
using Api;
using Microsoft.AspNetCore.Mvc;

namespace Tests;

public class TaskValidationTests
{
    private readonly HttpClient _client;

    public TaskValidationTests()
    {
        var factory = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostInvalidTaskReturns400ProblemDetails()
    {
        //Arrage
        var invalidTask = new TaskCreateDto
        {
            Title = "",
            Description = "test",
            DueDate = null,
            Status = "Open",
            Priority = "High"
        };

        //Act
        var response = await _client.PostAsJsonAsync("/api/tasks", invalidTask);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        //Assert
        Assert.NotNull(problem);
         Assert.Equal("Validation failed", problem!.Title);
        Assert.Contains("Title", problem.Errors.Keys); //check that title is in one of of the errors
    }
}