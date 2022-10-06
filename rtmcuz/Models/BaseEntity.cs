using System.ComponentModel.DataAnnotations;

namespace rtmcuz.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        //[Display(Name = "Дата создания")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTimeOffset? CreatedDate { get; set; }
        //[Display(Name = "Дата обновления")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
