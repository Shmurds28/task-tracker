using Application.DTO;
using AutoMapper;
using Domain.Model;

namespace Api.Mapping;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        // Domain -> DTO
        CreateMap<TaskItem, TaskDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()));

        // DTO -> Domain
        CreateMap<TaskCreateDto, TaskItem>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<Domain.TaskStatus>(src.Status!, true)))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => Enum.Parse<Domain.TaskPriority>(src.Priority!, true)));
    }
}