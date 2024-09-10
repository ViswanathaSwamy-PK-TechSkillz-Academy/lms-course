using AutoMapper;
using LMS.Data.Entities;
using LMS.Persistence;
using LMS.Web.Models.LeaveTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Controllers
{
    public class LeaveTypesController(LMSDbContext lmsDbContext, ILogger<LeaveTypesController> logger, IMapper mapper) : Controller
    {
        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            logger.LogInformation("LeaveTypes page visited at {time}", DateTime.Now);

            List<LeaveType> leaveTypes = await lmsDbContext.LeaveTypes.ToListAsync();

            IEnumerable<LeaveTypeReadOnlyVM> leaveTypeVMs = mapper.Map<IEnumerable<LeaveTypeReadOnlyVM>>(leaveTypes);

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

            LeaveType? leaveType = await lmsDbContext.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            LeaveTypeReadOnlyVM leaveTypeReadOnlyVM = mapper.Map<LeaveTypeReadOnlyVM>(leaveType);

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

            if (ModelState.IsValid)
            {
                LeaveType leaveType = mapper.Map<LeaveType>(leaveTypeCreateVM);

                lmsDbContext.Add(leaveType);
                await lmsDbContext.SaveChangesAsync();

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

            LeaveType? leaveType = await lmsDbContext.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            LeaveTypeEditVM leaveTypeEditVM = mapper.Map<LeaveTypeEditVM>(leaveType);
            return View(leaveTypeEditVM);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,NumberOfDays,Id")] LeaveType leaveType)
        {
            logger.LogInformation("LeaveType edited at {time}", DateTime.Now);

            if (id != leaveType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lmsDbContext.Update(leaveType);
                    await lmsDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTypeExists(leaveType.Id))
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
            return View(leaveType);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            logger.LogInformation("LeaveType delete page visited at {time}", DateTime.Now);

            if (id == null)
            {
                return NotFound();
            }

            LeaveType? leaveType = await lmsDbContext.LeaveTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            logger.LogInformation("LeaveType deleted at {time}", DateTime.Now);

            LeaveType? leaveType = await lmsDbContext.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                lmsDbContext.LeaveTypes.Remove(leaveType);
            }

            await lmsDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTypeExists(int id)
        {
            logger.LogInformation("Checking if LeaveType exists at {time}", DateTime.Now);
            return lmsDbContext.LeaveTypes.Any(e => e.Id == id);
        }
    }
}
