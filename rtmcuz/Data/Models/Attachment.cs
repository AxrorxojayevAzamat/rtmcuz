using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rtmcuz.Data.Models
{
    [Table("Attachments")]
    public partial class Attachment : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        [Display(Name = "HashName")]
        public string HashName { get; set; } = null!;
        [StringLength(255)]
        [Display(Name = "Path")]
        public string Path { get; set; } = null!;
        [StringLength(255)]
        [Display(Name = "OriginName")]
        public string OriginName { get; set; } = null!;
        [StringLength(255)]
        [Display(Name = "Extension")]
        public string Extension { get; set; } = null!;
        [InverseProperty("Image")]
        public virtual Section? Section { get; set; }
    }
}