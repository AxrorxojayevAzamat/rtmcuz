using rtmcuz.Data.Models;

namespace rtmcuz.ViewModels
{
    public class Document : BaseViewModel
    {
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
