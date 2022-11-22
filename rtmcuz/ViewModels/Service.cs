using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class Service : BaseViewModel
    {
        public string Subtitle { get; set; }
        public string Url { get; set; }
        [Required]
        public int? ImageId { get; set; }
        public Attachment? Image { get; set; }
        public static Service FromSection(Section section)
        {
            return new Service()
            {
                Id = section.Id,
                Title = section.Title,
                Subtitle = section.Subtitle,
                ImageId = section.ImageId,
                Image = section.Image,
                GroupId = section.GroupId,
                Lang = section.Lang,
            };
        }
    }
}
