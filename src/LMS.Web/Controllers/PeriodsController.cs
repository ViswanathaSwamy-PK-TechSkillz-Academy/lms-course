using LMS.Data.Entities;
using LMS.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Web.Controllers;

[Authorize(Roles = Roles.Administrator)]
public class PeriodsController : Controller
{
    private readonly LMSDbContext _context;

    public PeriodsController(LMSDbContext context)
    {
        _context = context;
    }

    // GET: Periods
    public async Task<IActionResult> Index()
    {
        return View(await _context.Periods.ToListAsync());
    }

    // GET: Periods/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var period = await _context.Periods
            .FirstOrDefaultAsync(m => m.Id == id);
        if (period == null)
        {
            return NotFound();
        }

        return View(period);
    }

    // GET: Periods/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Periods/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,StartDate,EndDate,Id")] Period period)
    {
        if (ModelState.IsValid)
        {
            _context.Add(period);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(period);
    }

    // GET: Periods/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var period = await _context.Periods.FindAsync(id);
        if (period == null)
        {
            return NotFound();
        }
        return View(period);
    }

    // POST: Periods/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Name,StartDate,EndDate,Id")] Period period)
    {
        if (id != period.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(period);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodExists(period.Id))
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
        return View(period);
    }

    // GET: Periods/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var period = await _context.Periods
            .FirstOrDefaultAsync(m => m.Id == id);
        if (period == null)
        {
            return NotFound();
        }

        return View(period);
    }

    // POST: Periods/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var period = await _context.Periods.FindAsync(id);
        if (period != null)
        {
            _context.Periods.Remove(period);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PeriodExists(int id)
    {
        return _context.Periods.Any(e => e.Id == id);
    }
}
