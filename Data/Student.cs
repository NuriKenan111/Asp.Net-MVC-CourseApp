using System.ComponentModel.DataAnnotations;

namespace EfCoreApp.Data;

public class Student
{
    [Key]
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? StudentSurname { get; set; }
    public string? StudentEmail { get; set; }
    public string? StudentPhone { get; set; }


}