using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rtmcuz.ViewModels
{
    public class Banner : BaseViewModel
    {
        public string Subtitle { get; set; }
        public string? Url { get; set; }
        [Required]
        public int? ImageId { get; set; }
        public Attachment? Image { get; set; }
        public static Banner FromSection(Section section)
        {
            return new Banner()
            {
                Id = section.Id,
                Title = section.Title,
                Subtitle = section.Subtitle,
                ImageId = section.ImageId,
                Url = section.Url,
                Image = section.Image,
                GroupId = section.GroupId,
                Lang = section.Lang,
            };
        }
    }
}
