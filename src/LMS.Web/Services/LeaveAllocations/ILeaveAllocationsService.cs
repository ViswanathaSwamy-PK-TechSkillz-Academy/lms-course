using LMS.Web.Models.LeaveAllocations;

namespace LMS.Web.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeave(string employeeId);

    Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId);

    Task<List<EmployeeListVM>> GetEmployees();

    Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId);

    //Task EditAllocation(LeaveAllocationEditVM allocationEditVm);
    //Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId);
}