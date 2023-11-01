using EfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreApp.Controllers;

public class CourseController : Controller
{
    private readonly DataContext _context;

    public CourseController(DataContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
   public async Task<IActionResult> Create(Student student)
   {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
   }
}