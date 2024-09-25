using AutoMapper;
using LMS.Data.Entities;
using LMS.Persistence;
using LMS.Web.Models.LeaveAllocations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Services.LeaveAllocations;

public class LeaveAllocationsService(LMSDbContext lmsDbContext, IHttpContextAccessor httpContextAccessor,
    UserManager<ApplicationUser> userManager, IMapper mapper) : ILeaveAllocationsService
{
    public async Task AllocateLeave(string employeeId)
    {
        // get all the leave types
        List<LeaveType> leaveTypes = await lmsDbContext.LeaveTypes.ToListAsync();

        // get the current period based on the year
        int currentYear = DateTime.Now.Year;
        Period? period = await lmsDbContext.Periods.SingleAsync(r => r.EndDate.Year == currentYear);

        int monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

        // foreach leave type, create an allocation entry
        foreach (var leaveType in leaveTypes)
        {
            decimal accrualRate = decimal.Divide(leaveType.NumberOfDays, 12);

            LeaveAllocation leaveAllocation = new()
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                Days = (int)Math.Ceiling(accrualRate * monthsRemaining)
            };

            lmsDbContext.Add(leaveAllocation);
        }

        await lmsDbContext.SaveChangesAsync();
    }

    public async Task<List<LeaveAllocation>> GetAllocations(string? userId)
    {
        string employeeId = string.Empty;

        if (string.IsNullOrEmpty(userId))
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User!);
            employeeId = user!.Id;
        }
        else
        {
            employeeId = userId!;
        }

        var period = await lmsDbContext.Periods.SingleAsync(r => r.EndDate.Year >= DateTime.Now.Year);

        var leaveAllocations = await lmsDbContext.LeaveAllocations
            .Include(r => r.LeaveType)
            .Include(r => r.Period)
            .Where(r => r.EmployeeId == employeeId && r.Period!.Id == period.Id)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
    {
        var allocations = await GetAllocations();

        var allocationsVmList = mapper.Map<List<LeaveAllocationVM>>(allocations);

        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User!);

        EmployeeAllocationVM employeeAllocationVm = new()
        {
            DateOfBirth = user!.DateOfBirth,

            Email = user.Email!,

            FirstName = user!.FirstName,

            LastName = user!.LastName,

            Id = user!.Id,

            LeaveAllocations = allocationsVmList
        };

        return employeeAllocationVm;
    }

    public async Task<List<EmployeeListVM>> GetEmployees()
    {
        var employees = await userManager.GetUsersInRoleAsync(Roles.Employee);

        var employeeListVm = mapper.Map<List<EmployeeListVM>>(employees);

        return employeeListVm;
    }

}
