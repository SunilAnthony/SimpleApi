using System.ComponentModel.DataAnnotations;

namespace SimpleApi.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? Name { get; set; }

        /* EF Relation */
        public int CourseId { get; set; }
        public IList<Course> Courses { get; set; }
    }
}