using Assessment.Application.ViewModels;
using Assessment.Domain.Models;
using AutoMapper;

namespace Assessment.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<EmployeeViewModel, Employee>();
                
        }
    }
}
