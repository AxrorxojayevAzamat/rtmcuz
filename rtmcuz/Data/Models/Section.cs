using rtmcuz.Data.Enums;
using rtmcuz.ViewModels;
using SlugGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rtmcuz.Data.Models
{
    public partial class Section : BaseEntity
    {
        public string Slug { get; set; }
        public Locales Lang { get; set; }
        public int GroupId { get; set; }
        public string? Content { get; set; }
        public string? Icon { get; set; }
        public string? Subtitle { get; set; }
        public string? Title { get; set; }
        public string? Template { get; set; }
        public SectionTypes Type { get; set; }
        public SectionStatus Status { get; set; }
        public string? Url { get; set; }
        public int? ParentId { get; set; }
        public int? ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        [InverseProperty("Section")]
        public Attachment? Image { get; set; }

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
                ImageId = news.ImageId,
                Subtitle = news.Subtitle,
                Content = news.Content,
                Type = SectionTypes.News,
            };
        }

        public static Section FromBanner(Banner banner)
        {
            return new Section()
            {
                Id = banner.Id,
                Title = banner.Title,
                Slug = banner.Title.GenerateSlug(),
                Subtitle = banner.Subtitle,
                Url = banner.Url,
                ImageId = banner.ImageId,
                Type = SectionTypes.Banner,
            };
        }

        public static Section FromDepartment(Department department)
        {
            return new Section()
            {
                Id = department.Id,
                Title = department.Title,
                Slug = department.Title.GenerateSlug(),
                Content = department.Content,
                Type = SectionTypes.Department,
            };
        }

        public static Section FromDocument(Document document)
        {
            return new Section()
            {
                Id = document.Id,
                Title = document.Title,
                Slug = document.Title.GenerateSlug(),
                Content = document.Content,
                Type = SectionTypes.Document,
            };
        }

        public static Section FromLeadership(Leadership leadership)
        {
            return new Section()
            {
                Id = leadership.Id,
                Title = leadership.Title,
                Slug = leadership.Title.GenerateSlug(),
                Subtitle = leadership.Subtitle,
                Content = leadership.Content,
                Type = SectionTypes.Leadership,
            };
        }

        public static Section FromQuestion(Question question)
        {
            return new Section()
            {
                Id = question.Id,
                Title = question.Title,
                Slug = question.Title.GenerateSlug(),
                Content = question.Content,
                Type = SectionTypes.Question,
            };
        }

        public static Section FromService(Service service)
        {
            return new Section()
            {
                Id = service.Id,
                Title = service.Title,
                Url = service.Url,
                Slug = service.Title.GenerateSlug(),
                Subtitle = service.Subtitle,
                ImageId = service.ImageId,
                Type = SectionTypes.Service,
            };
        }
    }
}
