using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiCommunity.Models
{
    public class UpdateGroupPictureModel
    {
        public string CourseCode { get; set; }
        public HttpPostedFileBase GroupPicture { get; set; }
    }
}