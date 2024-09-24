using LMS.Data.Entities;

namespace LMS.Web.Services.LeaveAllocations;

public interface ILeaveAllocationsService
{
    Task AllocateLeave(string employeeId);

    Task<List<LeaveAllocation>> GetEmployeeAllocations(string? employeeId);

    //Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId);
    //Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId);
    //Task<List<EmployeeListVM>> GetEmployees();
    //Task EditAllocation(LeaveAllocationEditVM allocationEditVm);
    //Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId);
}