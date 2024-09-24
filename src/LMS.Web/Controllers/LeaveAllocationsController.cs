using LMS.Web.Services.LeaveAllocations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers;

[Authorize]
public class LeaveAllocationsController(ILeaveAllocationsService leaveAllocationsService) : Controller
{

    public async Task<IActionResult> Index()
    {
        var leaveAllocations = await leaveAllocationsService.GetEmployeeAllocations();

        return View(leaveAllocations);
    }
}
