
using LMS.Data.Entities;
using LMS.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Services.LeaveAllocations;

public class LeaveAllocationsService(LMSDbContext lmsDbContext) : ILeaveAllocationsService
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
            decimal accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);

            LeaveAllocation leaveAllocation = new()
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                Days = (int)Math.Ceiling(accuralRate * monthsRemaining)
            };

            lmsDbContext.Add(leaveAllocation);
        }

        await lmsDbContext.SaveChangesAsync();
    }
}
