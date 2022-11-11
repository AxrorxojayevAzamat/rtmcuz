using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class Banner
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
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
                Content = section.Content,
                ImageId = section.ImageId,
                Image = section.Image,
            };
        }
    }
}
