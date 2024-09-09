using LMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LMS.Web.Controllers;

public class HomeController(ILogger<HomeController> _logger) : Controller
{
    public IActionResult Index()
    {
        _logger.LogInformation("Home page visited at {time}", DateTime.Now);

        return View();
    }

    public IActionResult Privacy()
    {
        _logger.LogInformation("Privacy page visited at {time}", DateTime.Now);

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("An error occurred at {time}", DateTime.Now);

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
