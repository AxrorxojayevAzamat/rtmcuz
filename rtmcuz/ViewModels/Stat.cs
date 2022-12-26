using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace rtmcuz.ViewModels
{
    public class Stat : BaseViewModel
    {
        [Display(Name = "Subtitle")]
        public string Subtitle { get; set; }

        [Display(Name = "Icon")]
        public string Icon { get; set; }

        public static Stat FromSection(Section section)
        {
            return new Stat()
            {
                Id = section.Id,
                Title = section.Title,
                Subtitle = section.Subtitle,
                Icon = section.Icon,
                GroupId = section.GroupId,
                Lang = section.Lang,
            };
        }
    }
}
