using LMS.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Web.Controllers;

public class TestController : Controller
{
    public IActionResult Index()
    {
        TestViewModel data = new()
        {
            Name = "Student of MVC Mastery",
            DateOfBirth = new DateTime(1954, 12, 01)
        };

        return View(data);
    }
}
