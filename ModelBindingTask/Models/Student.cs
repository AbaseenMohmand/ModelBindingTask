using System.ComponentModel.DataAnnotations;

namespace ModelBindingTask.Models
{
    public class Student
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Class { get; set; }
    }
}
