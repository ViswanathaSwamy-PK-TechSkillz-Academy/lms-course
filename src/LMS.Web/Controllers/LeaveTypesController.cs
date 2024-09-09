using AutoMapper;
using LMS.Data.Entities;
using LMS.Persistence;
using LMS.Web.Models.LeaveTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Controllers
{
    public class LeaveTypesController(LMSDbContext _context, ILogger<LeaveTypesController> _logger, IMapper _mapper) : Controller
    {
        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("LeaveTypes page visited at {time}", DateTime.Now);

            List<LeaveType> leaveTypes = await _context.LeaveTypes.ToListAsync();

            IEnumerable<LeaveTypeReadOnlyVM> leaveTypeVMs = _mapper.Map<IEnumerable<LeaveTypeReadOnlyVM>>(leaveTypes);

            return View(leaveTypeVMs);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("LeaveType details page visited at {time}", DateTime.Now);

            if (id == null)
            {
                return NotFound();
            }

            LeaveType? leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            LeaveTypeReadOnlyVM leaveTypeReadOnlyVM = _mapper.Map<LeaveTypeReadOnlyVM>(leaveType);

            return View(leaveTypeReadOnlyVM);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            _logger.LogInformation("LeaveType create page visited at {time}", DateTime.Now);

            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,NumberOfDays")] LeaveTypeCreateVM leaveTypeCreateVM)
        {
            _logger.LogInformation("LeaveType created at {time}", DateTime.Now);

            if (ModelState.IsValid)
            {
                LeaveType leaveType = _mapper.Map<LeaveType>(leaveTypeCreateVM);

                _context.Add(leaveType);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(leaveTypeCreateVM);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("LeaveType edit page visited at {time}", DateTime.Now);

            if (id == null)
            {
                return NotFound();
            }

            LeaveType? leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,NumberOfDays,Id")] LeaveType leaveType)
        {
            _logger.LogInformation("LeaveType edited at {time}", DateTime.Now);

            if (id != leaveType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveType);
                    await _context.SaveChangesAsync();
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
            _logger.LogInformation("LeaveType delete page visited at {time}", DateTime.Now);

            if (id == null)
            {
                return NotFound();
            }

            LeaveType? leaveType = await _context.LeaveTypes
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
            _logger.LogInformation("LeaveType deleted at {time}", DateTime.Now);

            LeaveType? leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType != null)
            {
                _context.LeaveTypes.Remove(leaveType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTypeExists(int id)
        {
            _logger.LogInformation("Checking if LeaveType exists at {time}", DateTime.Now);
            return _context.LeaveTypes.Any(e => e.Id == id);
        }
    }
}
