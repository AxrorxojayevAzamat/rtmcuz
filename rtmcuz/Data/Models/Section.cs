using rtmcuz.Data.Enums;
using rtmcuz.FormModels;
using SlugGenerator;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.Data.Models
{
    public partial class Section : BaseEntity
    {
        public string Slug { get; set; }
        public Locales Lang { get; set; }
        public int GroupId { get; set; }
        public string? Content { get; set; }
        public string? Icon { get; set; }
        public string? Image { get; set; }
        public string? Subtitle { get; set; }
        public string? Title { get; set; }
        public string? Template { get; set; }
        public SectionTypes Type { get; set; }
        public SectionStatus Status { get; set; }
        public string? Url { get; set; }
        public int? ParentId { get; set; }

        public static Section FromInteractive(Interactive interactive)
        {
            return new Section()
            {
                Id = interactive.Id,
                Slug = interactive.Title.GenerateSlug(),
                Title = interactive.Title,
                Subtitle = interactive.Subtitle,
                Icon = interactive.Icon,
                Type = SectionTypes.InterActive,
            };
        }

        public static Section FromNews(News news)
        {
            return new Section()
            {
                Id = news.Id,
                Slug = news.Title.GenerateSlug(),
                Title = news.Title,
                Image = news.Image,
                Subtitle = news.Subtitle,
                Content = news.Content,
                Type = SectionTypes.News,
            };
        }
    }
}
