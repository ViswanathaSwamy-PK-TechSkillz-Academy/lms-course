using AutoMapper;
using LMS.Data.Entities;
using LMS.Web.Models.LeaveTypes;

namespace LMS.Web.MappingProfiles;

public class LeaveTypeAutoMapperProfile : Profile
{
    public LeaveTypeAutoMapperProfile()
    {
        _ = CreateMap<LeaveType, LeaveTypeReadOnlyVM>();

        _ = CreateMap<LeaveTypeCreateVM, LeaveType>();

        _ = CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
    }
}

//.ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays));