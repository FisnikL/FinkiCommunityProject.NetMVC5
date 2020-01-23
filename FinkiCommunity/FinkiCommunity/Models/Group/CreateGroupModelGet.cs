using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinkiCommunity.Models
{
    public class CreateGroupModelGet
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public List<string> StudyYear { get; set; }
        public List<string> Semester { get; set; }
        public List<string> CourseType { get; set; }
        public List<string> StudyPrograms { get; set; }

        public CreateGroupModelGet()
        {
            StudyYear = new List<string>();
            Semester = new List<string>();
            CourseType = new List<string>();
            StudyPrograms = new List<string>();
        }
    }
}