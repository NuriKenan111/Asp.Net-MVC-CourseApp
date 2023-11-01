using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EfCoreApp.Models;

namespace EfCoreApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
