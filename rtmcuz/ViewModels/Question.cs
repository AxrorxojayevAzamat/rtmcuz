using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class Question : BaseViewModel
    {
        [Display(Name = "Question")]
        public override string Title { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        public static Question FromSection(Section section)
        {
            return new Question()
            {
                Id = section.Id,
                Title = section.Title,
                Content = section.Content,
                GroupId = section.GroupId,
                Lang = section.Lang,
            };
        }
    }
}
