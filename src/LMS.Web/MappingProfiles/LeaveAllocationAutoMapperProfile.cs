using AutoMapper;
using LMS.Data.Entities;
using LMS.Web.Models.LeaveAllocations;
using LMS.Web.Models.Periods;

namespace LMS.Web.MappingProfiles;

public class LeaveAllocationAutoMapperProfile : Profile
{
    public LeaveAllocationAutoMapperProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationVM>();

        CreateMap<LeaveAllocation, LeaveAllocationEditVM>();

        CreateMap<ApplicationUser, EmployeeListVM>();

        CreateMap<Period, PeriodVM>();
    }
}