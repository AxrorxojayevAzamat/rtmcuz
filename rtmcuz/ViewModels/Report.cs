using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class Report : BaseViewModel
    {

        [Display(Name = "Url")]
        public string Url { get; set; }

        public static Report FromSection(Section section)
        {
            return new Report()
            {
                Id = section.Id,
                Title = section.Title,
                Url = section.Url,
                GroupId = section.GroupId,
                Lang = section.Lang,
            };
        }
    }
}
