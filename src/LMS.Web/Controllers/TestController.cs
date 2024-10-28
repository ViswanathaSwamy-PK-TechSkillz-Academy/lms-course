using LMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers;

public class TestController(ILogger<TestController> logger) : Controller
{
    public IActionResult Index()
    {

        logger.LogInformation("Test page visited at {time}", DateTime.Now);

        TestViewModel data = new()
        {
            Name = "Student of MVC Mastery",
            DateOfBirth = new DateTime(1954, 12, 01)
        };

        logger.LogInformation("Returning data from Test page");

        return View(data);
    }
}

// Avoid in for Reference Types: Do not use the in keyword for reference types like classes or interfaces, as they are already passed by reference, and adding in can lead to runtime errors.
//public class TestController(in ILogger<TestController> logger) : Controller
//{
//    private readonly ILogger<TestController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

//    public IActionResult Index()
//    {

//        _logger.LogInformation("Test page visited at {time}", DateTime.Now);

//        TestViewModel data = new()
//        {
//            Name = "Student of MVC Mastery",
//            DateOfBirth = new DateTime(1954, 12, 01)
//        };

//        _logger.LogInformation("Returning data from Test page");

//        return View(data);
//    }
//}
