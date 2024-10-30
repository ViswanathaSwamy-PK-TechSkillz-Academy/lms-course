using LMS.Web.Models.LeaveAllocations;
using LMS.Web.Services.LeaveAllocations;
using LMS.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers;

[Authorize]
public class LeaveAllocationsController(ILeaveAllocationsService leaveAllocationsService
    , ILeaveTypesService leaveTypesService) : Controller
{
    [Authorize(Roles = Roles.Administrator)]
    public async Task<IActionResult> Index()
    {
        var employees = await leaveAllocationsService.GetEmployees();

        return View(employees);
    }

    public async Task<IActionResult> Details(string? userId)
    {
        var employeeVm = await leaveAllocationsService.GetEmployeeAllocations(userId);

        return View(employeeVm);
    }

    [Authorize(Roles = Roles.Administrator)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AllocateLeave(string id)
    {
        await leaveAllocationsService.AllocateLeave(id);

        return RedirectToAction(nameof(Details), new { id });
    }

    public async Task<IActionResult> EditAllocation(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var allocation = await leaveAllocationsService.GetEmployeeAllocation(id.Value);
        if (allocation == null)
        {
            return NotFound();
        }

        return View(allocation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM allocation)
    {
        if (await leaveTypesService.DaysExceedMaximum(allocation.LeaveType.Id, allocation.Days))
        {
            ModelState.AddModelError("Days", "Number of days exceeds the maximum days allowed");
        }

        if (ModelState.IsValid == false)
        {
            return View(allocation);
        }

        await leaveAllocationsService.EditAllocation(allocation);

        return RedirectToAction(nameof(Details), new { userId = allocation?.Employee?.Id });
    }

}
