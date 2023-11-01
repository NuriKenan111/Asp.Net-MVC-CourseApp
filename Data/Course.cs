using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace EfCoreApp.Data;

public class Course
{
    [Key]
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? CourseDescription { get; set; }
    public ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();
}