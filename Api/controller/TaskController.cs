using Api.Helpers;
using Application.DTO;
using Application.service;
using AutoMapper;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Api.controller;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;
    private readonly IMapper _mapper;

    public TaskController(ITaskService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? q, [FromQuery] string? sort = "dueDate:asc")
    {
        var tasks = await _service.GetAllAsync(q, sort);
        var result = _mapper.Map<IEnumerable<TaskDto>>(tasks);
        
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _service.GetByIdAsync(id);
        if (task == null)
            return NotFound(new ProblemDetails
            {
                Type = "https://example.com/probs/not-found",
                Title = "Task not found",
                Status = StatusCodes.Status404NotFound,
                Detail = $"Task with id {id} was not found."
            });

        return Ok(_mapper.Map<TaskDto>(task));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskCreateDto dto)
    {
        var errors = ValidationHelper.ValidateTaskDto(dto);
        if (errors.Any())
        {
            var details = new ValidationProblemDetails(errors)
            {
                Type = "https://example.com/probs/validation",
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest
            };
            return BadRequest(details);
        }

        var domain = _mapper.Map<TaskItem>(dto);
        var created = await _service.CreateAsync(domain);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, _mapper.Map<TaskDto>(created));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskCreateDto dto)
    {
        var errors = ValidationHelper.ValidateTaskDto(dto);
        if (errors.Any())
        {
            var details = new ValidationProblemDetails(errors)
            {
                Type = "https://example.com/probs/validation",
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest
            };
            return BadRequest(details);
        }

        var domain = _mapper.Map<TaskItem>(dto);
        var updated = await _service.UpdateAsync(id, domain);
        if (updated == null)
        {
            return NotFound(new ProblemDetails
            {
                Type = "https://example.com/probs/not-found",
                Title = "Task not found",
                Status = StatusCodes.Status404NotFound,
                Detail = $"Task with id {id} was not found."
            });
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new ProblemDetails
            {
                Type = "https://example.com/probs/not-found",
                Title = "Task not found",
                Status = StatusCodes.Status404NotFound,
                Detail = $"Task with id {id} was not found."
            });
        }
        return NoContent();
    }
}