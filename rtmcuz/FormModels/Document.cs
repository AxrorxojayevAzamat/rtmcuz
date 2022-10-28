using rtmcuz.Models;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.FormModels
{
    public class Document
    {
        [Key]
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
