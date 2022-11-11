using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public int? ImageId { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
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
            };
        }
    }
}
