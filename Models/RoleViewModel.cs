using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Job_Offers_Website.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [DisplayName("Name of Role")]
        public string Name { get; set; }
    }
}