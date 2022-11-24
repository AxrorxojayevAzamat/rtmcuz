using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class Department : BaseViewModel
    {
        [Display(Name = "Content")]
        public string Content { get; set; }
        public static Department FromSection(Section section)
        {
            return new Department()
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
