using rtmcuz.Data.Models;

namespace rtmcuz.ViewModels
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public static Question FromSection(Section section)
        {
            return new Question()
            {
                Id = section.Id,
                Title = section.Title,
                Content = section.Content,
            };
        }
    }
}
