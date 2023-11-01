using Microsoft.AspNetCore.Mvc;
using EfCoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace EfCoreApp.Controllers;
public class StudentController : Controller
{
    private readonly DataContext _context;

    public StudentController(DataContext context)
    {
        _context = context;
    }
   public IActionResult Create()
   {
      return View();
   }
   public async Task<IActionResult> Index()
   {
      var students = await _context.Students.ToListAsync();
      return View(students);
   }

   [HttpPost]
   public async Task<IActionResult> Create(Student student)
   {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
   }

   public async Task<IActionResult> Edit(int? id)
   {
        if(id == null)
        {
            return NotFound();
        }
        var student = await _context
        .Students
        .Include(c => c.CourseRegistrations)
        .ThenInclude(c => c.Course)
        .FirstOrDefaultAsync(c => c.StudentId == id);

        if(student == null)
        {
            return NotFound();
        }
        return View(student);
   }

   [HttpPost]
   [ValidateAntiForgeryToken]
   public async Task<IActionResult> Edit(int? id,Student model)
   {
        if(id != model.StudentId)
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
                if(!_context.Students.Any(c => c.StudentId == model.StudentId)) return NotFound();
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
        var student = await _context.Students.FindAsync(id);
        if(student == null)
        {
            return NotFound();
        }
        return View(student);
   }

   [HttpPost]
   public async Task<IActionResult> Delete([FromForm]int id)
   {
        var student = await _context.Students.FindAsync(id);
        if(student == null)
        {
            return NotFound();
        }
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
   }

}