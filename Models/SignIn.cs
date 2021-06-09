using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DawProject.Models
{
    public class SignIn
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class Roles
    { 
        [Key]
        public int e_id { get; set; }

        [Required]
        public int employee_id { get; set; }

        [Required]
        public string role_name { get; set; }

    }
}