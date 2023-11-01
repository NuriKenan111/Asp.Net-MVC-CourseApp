namespace EfCoreApp.Data;

public class CourseRegistration
{
    public int CourseRegistrationId { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public Student Student { get; set; } = default!;
    public Course Course { get; set; } = default!;
}