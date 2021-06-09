using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DawProject.Models
{ 
    public class Projects : IValidatableObject
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)] 
        public DateTime EndDate { get; set; }

        public string ProjectDescription { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    errorMessage: "DeadLine-ul trebuie sa fie mai mare decat StartDate-ul",
                    memberNames: new[] { "EndDate" }
               );
            }
            if(StartDate < DateTime.Today)
            {
                yield return new ValidationResult(
                  errorMessage: "Proiectul nu poate incepe in trecut!",
                  memberNames: new[] { "EndDate" }
             );
            }
        }
    }
    public class ProjectsAssigned
    {
        [Key]
        public int AssignID { get; set; }
        [Required]
        [DisplayName("Project Name")]
        public int ProjectId { get; set; }
        [Required]
        [DisplayName("Employee")]
        public int EmployeeId { get; set; }
    }
}