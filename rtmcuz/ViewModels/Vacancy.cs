using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class Vacancy : BaseViewModel
    {
        [Display(Name = "Content")]
        public string Content { get; set; }

        public static Vacancy FromSection(Section section)
        {
            return new Vacancy()
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
