using rtmcuz.Data.Models;

namespace rtmcuz.ViewModels
{
    public class Department : BaseViewModel
    {
        public string Content { get; set; }
        public static Department FromSection(Section section)
        {
            return new Department()
            {
                Id = section.Id,
                Title = section.Title,
                Content = section.Content,
            };
        }
    }
}
