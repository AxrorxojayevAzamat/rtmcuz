using rtmcuz.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.ViewModels
{
    public class Document : BaseViewModel
    {
        [Display(Name = "Content")]
        public string Content { get; set; }

        public static Document FromSection(Section section)
        {
            return new Document()
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
