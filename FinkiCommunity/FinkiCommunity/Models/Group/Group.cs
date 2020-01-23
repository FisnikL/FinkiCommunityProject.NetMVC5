using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinkiCommunity.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Code")]
        public string CourseCode { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string CourseName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string CourseDescription { get; set; }
        [Required]
        [Display(Name = "Study Year")]
        public Enums.StudyYear StudyYear { get; set; }
        [Required]
        public Enums.Semester Semester { get; set; }
        [Required]
        [Display(Name = "Course Type")]
        public Enums.CourseType CourseType { get; set; }
        [Required]
        [Display(Name = "Study Programs")]
        public ICollection<StudyProgram> StudyPrograms { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfReplies { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}