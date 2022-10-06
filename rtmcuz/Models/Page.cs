using rtmcuz.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace rtmcuz.Models
{
    public class Page : BaseEntity
    {
        public string Name { get; set; }
        public Locales Locale { get; set; }
        public string Slug { get; set; }
        public int OriginId { get; set; }
        public string Permalink { get; set; }
        public PageTypes PageType { get; set; }
        public string? Content { get; set; }
        public string? Template { get; set; }
    }
}
