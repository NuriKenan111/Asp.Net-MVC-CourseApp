using EfCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EfCoreApp.Controllers;

public class CourseController : Controller
{
    private readonly DataContext _context;

    public CourseController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
       var courses = await _context.Courses.ToListAsync();
       return View(courses);
    }

    public IActionResult Create()
    {
        return View();
    }
    
   [HttpPost]
   public async Task<IActionResult> Create(Course model)
   {
        _context.Courses.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
   }
    
   public async Task<IActionResult> Edit(int? id)
   {
        if(id == null)
        {
            return NotFound();
        }
        var course = await _context.Courses.FindAsync(id);
        if(course == null)
        {
            return NotFound();
        }
        return View(course);
   }

   [HttpPost]
   [ValidateAntiForgeryToken]
   public async Task<IActionResult> Edit(int? id,Course model)
   {
        if(id != model.CourseId)
        {
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!_context.Courses.Any(c => c.CourseId == model.CourseId)) return NotFound();
                else throw;
            }
            return RedirectToAction("Index");
        }
        return View(model);
   }

   public async Task<IActionResult> Delete(int? id)
   {
        if(id == null)
        {
            return NotFound();
        }
        var course = await _context.Courses.FindAsync(id);
        if(course == null)
        {
            return NotFound();
        }
        return View(course);
   }

   [HttpPost]
   [ValidateAntiForgeryToken]
   public async Task<IActionResult> Delete([FromForm]int id)
   {
        var course = await _context.Courses.FindAsync(id);
        if(course == null)
        {
            return NotFound();
        }
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
   }
}