using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace rtmcuz.ViewModels
{
    public class Interactive : BaseViewModel
    {
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
                GroupId = section.GroupId,
                Lang = section.Lang,
            };
        }
    }
}
