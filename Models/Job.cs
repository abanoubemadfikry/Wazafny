 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace Job_Offers_Website.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Display(Name ="Job Name")]
        public string JobTitle { get; set; }
        [DisplayName("Job Description")]
        public string JobContent { get; set; }

        [DisplayName("Job Photo")]
        public string JobImage { get; set; }

        [Display(Name ="Job Type")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<ApplyForJob> ApplyJobs { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser  User { get; set; }
    }
}