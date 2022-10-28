using rtmcuz.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.FormModels
{
    public class News
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }

        public static News FromSection(Section section)
        {
            return new News() { 
                Id = section.Id,
                Title = section.Title,
                Image = section.Image,
                Subtitle = section.Subtitle,
                Content = section.Content,
            };
        }
    }
}
