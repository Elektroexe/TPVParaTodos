using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Notification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public Type Type { get; set; }
    }

    public enum Type
    {
        Success,
        Warning,
        Error
    }
}