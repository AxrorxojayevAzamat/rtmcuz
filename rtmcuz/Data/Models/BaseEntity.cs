using System.ComponentModel.DataAnnotations;

namespace rtmcuz.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "CreatedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTimeOffset? CreatedDate { get; set; }

        [Display(Name = "UpdatedDate")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
