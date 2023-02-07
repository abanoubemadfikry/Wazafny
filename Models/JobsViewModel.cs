using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Job_Offers_Website.Models
{
    public class JobsViewModel
    {
        public string Jobtitle { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}