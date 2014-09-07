using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KudevolveWeb.ViewModels
{
    public class CommentViewModel
    {
        public string postid { get; set; }
        public string PostUser { get; set; }
        public string Content { get; set; }
    }
}