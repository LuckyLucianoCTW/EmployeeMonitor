using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DawProject.Models
{
    public class Logs
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string text_log { get; set; }
    }
}