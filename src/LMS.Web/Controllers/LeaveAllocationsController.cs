using LMS.Web.Services.LeaveAllocations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers;

[Authorize]
public class LeaveAllocationsController(ILeaveAllocationsService leaveAllocationsService) : Controller
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
    public async Task<IActionResult> AllocateLeave(string? userId)
    {
        await leaveAllocationsService.AllocateLeave(userId);

        return View();
    }

}
