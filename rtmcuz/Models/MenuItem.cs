namespace rtmcuz.Models
{
    public class MenuItem : BaseEntity
    {
        public int PageId { get; set; }
        public int? ParentId { get; set; }

        public Page Page { get; set; }
        public List<MenuItem>? Items { get; set; }
    }
}
