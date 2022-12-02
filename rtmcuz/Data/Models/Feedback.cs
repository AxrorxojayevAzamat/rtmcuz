using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rtmcuz.Data.Models
{
    public class Feedback : BaseEntity
    {

        [Display(Name = "Fullname")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string Title { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string Email { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        public string Description { get; set; }

        [Display(Name = "Department")]
        [Required(ErrorMessage = "SelectRequiredErrorMessage")]
        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Feedbacks")]
        [Display(Name = "Department")]
        public Section? Department { get; set; }
    }
}
