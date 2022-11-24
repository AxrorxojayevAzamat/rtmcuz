using rtmcuz.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "GroupId")]
        public int? GroupId { get; set; }

        [Display(Name = "Lang")]
        public Locales? Lang { get; set; }

        [Display(Name = "Title")]
        public virtual string Title { get; set; }
    }
}
