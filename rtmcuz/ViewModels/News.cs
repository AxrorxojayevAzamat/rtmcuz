using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class News : BaseViewModel
    {
        [Display(Name = "Subtitle")]
        public string Subtitle { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }
        [Required]

        [Display(Name = "ImageId")]
        public int? ImageId { get; set; }
        public Attachment? Image { get; set; }
        public static News FromSection(Section section)
        {
            return new News()
            {
                Id = section.Id,
                Title = section.Title,
                ImageId = section.ImageId,
                Subtitle = section.Subtitle,
                Content = section.Content,
                Image = section.Image,
                GroupId = section.GroupId,
                Lang = section.Lang,
            };
        }
    }
}
