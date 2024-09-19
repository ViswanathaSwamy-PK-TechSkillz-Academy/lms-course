using LMS.Web.Models.LeaveTypes;
using LMS.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Controllers;

[Authorize(Roles = Roles.Administrator)]
public class LeaveTypesController(ILeaveTypesService leaveTypesService, ILogger<LeaveTypesController> logger) : Controller
{
    private const string NameExistsValidationMessage = "This leave type already exists in the database";

    // GET: LeaveTypes
    public async Task<IActionResult> Index()
    {
        logger.LogInformation("LeaveTypes page visited at {time}", DateTime.Now);

        IEnumerable<LeaveTypeReadOnlyVM> leaveTypeVMs = await leaveTypesService.GetAllAsync();

        return View(leaveTypeVMs);
    }

    // GET: LeaveTypes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        logger.LogInformation("LeaveType details page visited at {time}", DateTime.Now);

        if (id == null)
        {
            return NotFound();
        }

        LeaveTypeReadOnlyVM? leaveTypeReadOnlyVM = await leaveTypesService.GetAsync<LeaveTypeReadOnlyVM>(id.Value);

        return View(leaveTypeReadOnlyVM);
    }

    // GET: LeaveTypes/Create
    public IActionResult Create()
    {
        logger.LogInformation("LeaveType create page visited at {time}", DateTime.Now);

        return View();
    }

    // POST: LeaveTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,NumberOfDays")] LeaveTypeCreateVM leaveTypeCreateVM)
    {
        logger.LogInformation("LeaveType created at {time}", DateTime.Now);

        // Adding custom validation and model state error
        if (leaveTypeCreateVM.Name.Contains("test", StringComparison.CurrentCultureIgnoreCase))
        {
            ModelState.AddModelError(nameof(leaveTypeCreateVM.Name), "Name cannot contain the word test");
        }

        if (await leaveTypesService.CheckIfLeaveTypeNameExists(leaveTypeCreateVM.Name))
        {
            ModelState.AddModelError(nameof(leaveTypeCreateVM.Name), "Name cannot contain the word test");
        }

        if (ModelState.IsValid)
        {
            await leaveTypesService.CreateAsync(leaveTypeCreateVM);

            return RedirectToAction(nameof(Index));
        }

        return View(leaveTypeCreateVM);
    }

    // GET: LeaveTypes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        logger.LogInformation("LeaveType edit page visited at {time}", DateTime.Now);

        if (id == null)
        {
            return NotFound();
        }

        LeaveTypeEditVM? leaveTypeEditVM = await leaveTypesService.GetAsync<LeaveTypeEditVM>(id.Value);
        if (leaveTypeEditVM == null)
        {
            return NotFound();
        }

        return View(leaveTypeEditVM);
    }

    // POST: LeaveTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Name,NumberOfDays,Id")] LeaveTypeEditVM leaveTypeEditVM)
    {
        logger.LogInformation("LeaveType edited at {time}", DateTime.Now);

        if (id != leaveTypeEditVM.Id)
        {
            return NotFound();
        }

        if (await leaveTypesService.CheckIfLeaveTypeNameExistsForEdit(leaveTypeEditVM))
        {
            ModelState.AddModelError(nameof(leaveTypeEditVM.Name), NameExistsValidationMessage);
        }

        if (ModelState.IsValid)
        {
            try
            {
                await leaveTypesService.EditAsync(leaveTypeEditVM);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!leaveTypesService.LeaveTypeExists(leaveTypeEditVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        return View(leaveTypeEditVM);
    }

    // GET: LeaveTypes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        logger.LogInformation("LeaveType delete page visited at {time}", DateTime.Now);

        if (id == null)
        {
            return NotFound();
        }

        LeaveTypeReadOnlyVM? leaveTypeReadOnlyVM = await leaveTypesService.GetAsync<LeaveTypeReadOnlyVM>(id.Value);
        if (leaveTypeReadOnlyVM == null)
        {
            return NotFound();
        }

        return View(leaveTypeReadOnlyVM);
    }

    // POST: LeaveTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        logger.LogInformation("LeaveType deleted at {time}", DateTime.Now);

        await leaveTypesService.RemoveAsync(id);

        return RedirectToAction(nameof(Index));
    }

}
