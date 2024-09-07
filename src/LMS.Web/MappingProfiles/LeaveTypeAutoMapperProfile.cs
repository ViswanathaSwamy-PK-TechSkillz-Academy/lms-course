using AutoMapper;
using LMS.Data.Entities;
using LMS.Web.Models.LeaveTypes;

namespace LMS.Web.MappingProfiles;

public class LeaveTypeAutoMapperProfile : Profile
{
    public LeaveTypeAutoMapperProfile()
    {
        CreateMap<LeaveType, IndexVM>().ReverseMap();
    }
}
