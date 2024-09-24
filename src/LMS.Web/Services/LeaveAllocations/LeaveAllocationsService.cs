using LMS.Data.Entities;
using LMS.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Services.LeaveAllocations;

public class LeaveAllocationsService(LMSDbContext lmsDbContext, IHttpContextAccessor httpContextAccessor,
    UserManager<ApplicationUser> userManager) : ILeaveAllocationsService
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

    public async Task<List<LeaveAllocation>> GetEmployeeAllocations()
    {
        var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User!);

        var leaveAllocations = await lmsDbContext.LeaveAllocations
            .Include(r => r.LeaveType)
            .Include(r => r.EmployeeId)
            .Include(r => r.Period)
            .Where(r => r.EmployeeId == user!.Id)
            .ToListAsync();

        return leaveAllocations;
    }
}
