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
        var leaveAllocations = await leaveAllocationsService.GetEmployeeAllocations();

        return View(leaveAllocations);
    }

    public async Task<IActionResult> Details()
    {
        var employeeVm = await leaveAllocationsService.GetEmployeeAllocations();

        return View(employeeVm);
    }
}
