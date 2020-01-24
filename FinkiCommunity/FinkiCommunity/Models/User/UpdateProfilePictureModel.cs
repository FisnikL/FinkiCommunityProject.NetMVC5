using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinkiCommunity.Models
{
    public class UpdateProfilePictureModel
    {
        public HttpPostedFileBase ProfilePicture { get; set; }
    }
}