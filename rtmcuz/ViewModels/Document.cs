using rtmcuz.Data.Models;

namespace rtmcuz.ViewModels
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public static Document FromSection(Section section)
        {
            return new Document()
            {
                Id = section.Id,
                Title = section.Title,
                Content = section.Content,
            };
        }
    }
}
