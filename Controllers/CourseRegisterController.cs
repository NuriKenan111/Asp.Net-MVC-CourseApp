using EfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfCoreApp.Controllers;

public class CourseRegisterController : Controller
{
    private readonly DataContext _context;

    public CourseRegisterController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(){
        var registrations = await _context.CourseRegistrations.Include(x => x.Student)
                                                                .Include(x => x.Course).
                                                                ToListAsync();
        return View(registrations);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Students = new SelectList(await _context.Students.ToListAsync(), "StudentId", "StudentNameSurname");
        ViewBag.Courses =  new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName");
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseRegistration model)
    {
        model.RegistrationDate = DateTime.Now;
        _context.CourseRegistrations.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}