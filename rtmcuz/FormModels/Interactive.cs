using rtmcuz.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.FormModels
{
    public class Interactive
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Icon { get; set; }

        public static Interactive FromSection(Section section)
        {
            return new Interactive()
            {
                Id = section.Id,
                Title = section.Title,
                Subtitle = section.Subtitle,
                Icon = section.Icon,
            };
        }
    }
}
