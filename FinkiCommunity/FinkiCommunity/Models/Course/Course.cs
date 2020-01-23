using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiCommunity.Models
{
    

    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CourseDescription { get; set; }
        public Enums.StudyYear StudyYear { get; set; }
        public Enums.Semester Semester { get; set; }
        public List<Enums.Program> Programs { get; set; }
        public string CourseType { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfReplies { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        

    }
}