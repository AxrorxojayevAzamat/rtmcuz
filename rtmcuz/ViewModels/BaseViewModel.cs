using rtmcuz.Data.Enums;

namespace rtmcuz.ViewModels
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public Locales? Lang { get; set; }
        public string Title { get; set; }
    }
}
