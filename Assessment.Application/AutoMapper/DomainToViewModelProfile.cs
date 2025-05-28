using Assessment.Application.ViewModels;
using Assessment.Domain.Models;
using AutoMapper;

namespace Assessment.Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Employee, EmployeeViewModel>();
        }
    }
}
