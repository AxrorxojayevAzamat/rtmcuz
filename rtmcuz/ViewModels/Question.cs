using rtmcuz.Data.Models;

namespace rtmcuz.ViewModels
{
    public class Question : BaseViewModel
    {
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
