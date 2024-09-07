using AutoMapper;
using LMS.Data.Entities;
using LMS.Persistence;
using LMS.Web.Models.LeaveTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Controllers
{
    public class LeaveTypesController(LMSDbContext context, ILogger<LeaveTypesController> logger, IMapper mapper) : Controller
    {
        private readonly LMSDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly ILogger<LeaveTypesController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("LeaveTypes page visited at {time}", DateTime.Now);

            List<LeaveType> leaveTypes = await _context.LeaveTypes.ToListAsync();

            IEnumerable<IndexVM> leaveTypeVMs = _mapper.Map<IEnumerable<IndexVM>>(leaveTypes);

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

            return View(leaveType);
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
        public async Task<IActionResult> Create([Bind("Name,NumberOfDays,Id")] LeaveType leaveType)
        {
            _logger.LogInformation("LeaveType created at {time}", DateTime.Now);

            if (ModelState.IsValid)
            {
                _context.Add(leaveType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveType);
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
