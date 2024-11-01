﻿using AutoMapper;
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
        List<LeaveType> leaveTypes = await lmsDbContext.LeaveTypes
            .Where(q => q.LeaveAllocations != null && !q.LeaveAllocations.Any(x => x.EmployeeId == employeeId))
            .ToListAsync();

        // get the current period based on the year
        int currentYear = DateTime.Now.Year;
        Period? period = await lmsDbContext.Periods.SingleAsync(r => r.EndDate.Year == currentYear);

        int monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

        // foreach leave type, create an allocation entry
        foreach (var leaveType in leaveTypes)
        {
            #region Works, but not best practice
            //var allocationExists = await AllocationExists(employeeId, period.Id, leaveType.Id);
            //if (allocationExists)
            //{
            //    continue;
            //}
            #endregion

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

    public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
    {
        var user = string.IsNullOrEmpty(userId)
            ? await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User!)
            : await userManager.FindByIdAsync(userId);

        var allocations = await GetAllocations(user?.Id);

        var allocationsVmList = mapper.Map<List<LeaveAllocationVM>>(allocations);

        var leaveTypesCount = await lmsDbContext.LeaveTypes.CountAsync();

        EmployeeAllocationVM employeeAllocationVm = new()
        {
            DateOfBirth = user!.DateOfBirth,
            Email = user.Email!,
            FirstName = user!.FirstName,
            LastName = user!.LastName,
            Id = user!.Id,
            LeaveAllocations = allocationsVmList,
            IsCompletedAllocation = leaveTypesCount == allocations.Count
        };

        return employeeAllocationVm;
    }

    public async Task<List<EmployeeListVM>> GetEmployees()
    {
        var employees = await userManager.GetUsersInRoleAsync(Roles.Employee);

        var employeeListVm = mapper.Map<List<EmployeeListVM>>(employees);

        return employeeListVm;
    }

    public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
    {
        var allocation = await lmsDbContext.LeaveAllocations
               .Include(q => q.LeaveType)
               .Include(q => q.Employee)
               .FirstOrDefaultAsync(q => q.Id == allocationId);

        var model = mapper.Map<LeaveAllocationEditVM>(allocation);

        return model;
    }

    public async Task EditAllocation(LeaveAllocationEditVM allocationEditVM)
    {
        //var leaveAllocation = await GetEmployeeAllocation(allocationEditVM.Id) ?? throw new Exception("Leave allocation record does not exist.");

        //leaveAllocation.Days = allocationEditVM.Days;
        //option 1 // lmsDbContext.Update(leaveAllocation);
        //option 2 // lmsDbContext.Entry(leaveAllocation).State = EntityState.Modified;
        // await lmsDbContext.SaveChangesAsync();

        await lmsDbContext.LeaveAllocations
            .Where(q => q.Id == allocationEditVM.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVM.Days));
    }

    private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
    {
        var leaveAllocations = await lmsDbContext.LeaveAllocations
            .Include(r => r.LeaveType)
            .Include(r => r.Period)
            .Where(r => r.EmployeeId == userId && r.Period!.EndDate.Year == DateTime.Now.Year)
            .ToListAsync();

        return leaveAllocations;
    }

    private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
    {
        var exists = await lmsDbContext.LeaveAllocations.AnyAsync(q =>
            q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId && q.PeriodId == periodId
        );

        return exists;
    }

}
