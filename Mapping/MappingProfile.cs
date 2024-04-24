using AutoMapper;
using ServiceBasedApplication.Models;
using ServiceBasedApplication.ViewModels;

namespace ServiceBasedApplication.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Course, CourseViewModel>();
        CreateMap<CourseViewModel, Course>();
    }
}
