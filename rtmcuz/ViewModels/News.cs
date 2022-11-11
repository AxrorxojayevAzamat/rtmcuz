using rtmcuz.Data.Models;

namespace rtmcuz.ViewModels
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }

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
