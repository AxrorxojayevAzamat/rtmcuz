using rtmcuz.Data.Enums;
using rtmcuz.ViewModels;
using SlugGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rtmcuz.Data.Models
{
    public partial class Section : BaseEntity
    {

        [Display(Name = "Slug")]
        public string Slug { get; set; }

        [Display(Name = "Lang")]
        public Locales? Lang { get; set; }

        [Display(Name = "GroupId")]
        public int? GroupId { get; set; }

        [Display(Name = "Content")]
        public string? Content { get; set; }

        [Display(Name = "Icon")]
        public string? Icon { get; set; }

        [Display(Name = "Subtitle")]
        public string? Subtitle { get; set; }

        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Template")]
        public string? Template { get; set; }

        [Display(Name = "Type")]
        public SectionTypes Type { get; set; }

        [Display(Name = "Status")]
        public SectionStatus Status { get; set; }

        [Display(Name = "Url")]
        public string? Url { get; set; }

        [Display(Name = "ParentId")]
        public int? ParentId { get; set; }

        [Display(Name = "ImageId")]
        public int? ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        [InverseProperty("Section")]
        public Attachment? Image { get; set; }

        [InverseProperty("Department")]
        public virtual List<Feedback>? Feedbacks { get; set; }

        public static Section FromInteractive(Interactive interactive)
        {
            return new Section()
            {
                Id = interactive.Id,
                GroupId = interactive.GroupId,
                Lang = interactive.Lang,
                Slug = interactive.Title.GenerateSlug(),
                Title = interactive.Title,
                Subtitle = interactive.Subtitle,
                Url = interactive.Url,
                Icon = interactive.Icon,
                Type = SectionTypes.Interactive,
            };
        }

        public static Section FromNews(News news)
        {
            return new Section()
            {
                Id = news.Id,
                GroupId = news.GroupId,
                Lang = news.Lang,
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
                GroupId = banner.GroupId,
                Lang = banner.Lang,
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
                GroupId = department.GroupId,
                Lang = department.Lang,
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
                GroupId = document.GroupId,
                Lang = document.Lang,
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
                GroupId = leadership.GroupId,
                Lang = leadership.Lang,
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
                GroupId = question.GroupId,
                Lang = question.Lang,
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
                GroupId = service.GroupId,
                Lang = service.Lang,
                Title = service.Title,
                Url = service.Url,
                Slug = service.Title.GenerateSlug(),
                Subtitle = service.Subtitle,
                ImageId = service.ImageId,
                Type = SectionTypes.Service,
            };
        }

        public static Section FromVacancy(Vacancy vacancy)
        {
            return new Section()
            {
                Id = vacancy.Id,
                GroupId = vacancy.GroupId,
                Lang = vacancy.Lang,
                Title = vacancy.Title,
                Slug = vacancy.Title.GenerateSlug(),
                Content = vacancy.Content,
                Type = SectionTypes.Vacancy,
            };
        }

        public static Section FromReport(Report report)
        {
            return new Section()
            {
                Id = report.Id,
                GroupId = report.GroupId,
                Lang = report.Lang,
                Title = report.Title,
                Url = report.Url,
                Slug = report.Title.GenerateSlug(),
                Type = SectionTypes.Report,
            };
        }
                public static Section FromStat(Stat stat)
        {
            return new Section()
            {
                Id = stat.Id,
                GroupId = stat.GroupId,
                Lang = stat.Lang,
                Slug = stat.Title.GenerateSlug(),
                Title = stat.Title,
                Subtitle = stat.Subtitle,
                Icon = stat.Icon,
                Type = SectionTypes.Stat,
            };
        }
    }
}
