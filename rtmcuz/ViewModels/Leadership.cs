using rtmcuz.Data.Models;

namespace rtmcuz.ViewModels
{
    public class Leadership
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }

        public static Leadership FromSection(Section section)
        {
            return new Leadership()
            {
                Id = section.Id,
                Title = section.Title,
                Subtitle = section.Subtitle,
                Content = section.Content,
            };
        }

    }
}
