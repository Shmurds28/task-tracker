using Application.DTO;
using Domain;

namespace Api.Helpers;

public static class ValidationHelper
{
    public static Dictionary<string, string[]> ValidateTaskDto(TaskCreateDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        if (dto == null)
        {
            errors["body"] = new[] { "Request body is required." };
            return errors;
        }

        if (string.IsNullOrWhiteSpace(dto.Title))
            errors[nameof(dto.Title)] = new[] { "Title is required and cannot be empty." };

        if (string.IsNullOrWhiteSpace(dto.Status) || !Enum.TryParse<Domain.TaskStatus>(dto.Status!, true, out _))
            errors[nameof(dto.Status)] = new[] { "Status must be one of: New, InProgress, Done." };

        if (string.IsNullOrWhiteSpace(dto.Priority) || !Enum.TryParse<TaskPriority>(dto.Priority!, true, out _))
            errors[nameof(dto.Priority)] = new[] { "Priority must be one of: Low, Medium, High." };

        if (dto.DueDate.HasValue && dto.DueDate.Value < DateTime.UtcNow.AddYears(-100))
            errors[nameof(dto.DueDate)] = new[] { "DueDate must be a valid date." };

        return errors;
    }
}