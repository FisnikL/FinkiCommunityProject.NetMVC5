using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiCommunity.Models
{
    public class CreateGroupModelPost
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string StudyYear { get; set; }
        public string Semester { get; set; }
        public string CourseType { get; set; }
        public List<string> StudyPrograms { get; set; }

        public CreateGroupModelPost()
        {
            StudyPrograms = new List<string>();
        }
    }
}