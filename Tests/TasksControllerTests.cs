using System.Linq;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Api;
using Application.DTO;
using Xunit;

namespace Tests;

public class TasksControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TasksControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTasksTest()
    {
        // Act
        var result = await _client.GetFromJsonAsync<List<TaskDto>>("/api/tasks");

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count == 3);
        Assert.All(result, t => Assert.False(string.IsNullOrWhiteSpace(t.Title)));
    }
}