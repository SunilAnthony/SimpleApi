using System.ComponentModel.DataAnnotations;

namespace SimpleApi.Models
{
    public class Course
    {

        public int CourseId { get; set; }
        public string? CourseName { get; set; }

         /* EF Relations */
        public IEnumerable<Student> Students { get; set; }
    }
}