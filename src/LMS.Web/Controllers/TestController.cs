using LMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers;

public class TestController(ILogger<TestController> _logger) : Controller
{

    public IActionResult Index()
    {

        _logger.LogInformation("Test page visited at {time}", DateTime.Now);

        TestViewModel data = new()
        {
            Name = "Student of MVC Mastery",
            DateOfBirth = new DateTime(1954, 12, 01)
        };

        _logger.LogInformation("Returning data from Test page");

        return View(data);
    }
}
