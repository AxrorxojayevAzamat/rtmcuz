using Microsoft.EntityFrameworkCore;
using rtmcuz.Data;
using rtmcuz.Data.Enums;
using rtmcuz.Data.Models;
using rtmcuz.Interfaces;
using System.Globalization;

namespace rtmcuz.Repositories
{
    public class SectionRespository : ISectionRepository
    {
        private readonly RtmcUzContext _context;
        private readonly IAttachmentService _attachmentService;
        private readonly Locales _locale;
        public SectionRespository(RtmcUzContext context, IAttachmentService attachmentService)
        {
            _context = context;
            _attachmentService = attachmentService;
            _locale = (Locales)Enum.Parse(typeof(Locales), CultureInfo.CurrentCulture.Name.Replace('-', '_'));
        }

        public List<Section> ListItems(SectionTypes type) => _context.Sections.Where(s => s.Type == type && s.Lang == _locale).ToList();

        public async Task<List<Section>> ListItemsAsync(SectionTypes type) => await _context.Sections.Include(s => s.Image).Where(s => s.Type == type && s.Lang == _locale).ToListAsync();

        public void Create(Section section, IFormFile? image = null)
        {
            if (image != null)
            {
                section.ImageId = _attachmentService.UploadFileToStorage(image);
            }

            section.Lang = _locale;
            section.GroupId = _GetGroupId(section);
            _context.Update(section);
            _context.SaveChanges();
        }

        public void Save(Section section, IFormFile? image = null)
        {
            if (image != null)
            {
                section.ImageId = _attachmentService.UploadFileToStorage(image);
            }
            _context.Update(section);
            _context.SaveChanges();
        }

        public Section GetItem(int? id) => _context.Sections.Include(s => s.Image).Where(a => a.Id == id).FirstOrDefault();

        public async Task<Section> GetItemAsync(int? id) => await _context.Sections.Include(s => s.Image).Where(a => a.Id == id).FirstOrDefaultAsync();

        public async Task DeleteConfirmed(int id)
        {
            var item = await _context.Sections.FindAsync(id);
            if (item != null)
            {
                _context.Sections.Remove(item);
            }
            _context.SaveChanges();
        }

        public bool Exists(int? id, SectionTypes type) => id == null || _context.Sections.Where(s => s.Type == type) == null;

        public bool IsNull(SectionTypes type) => _context.Sections.Where(s => s.Type == type) == null;

        public bool Any(int id) => _context.Sections.Any(s => s.Id == id);

        public Dictionary<string, Section> VariantsList(int groupId)
        {
            var variants = new Dictionary<string, Section>();
            foreach (var lang in Enum.GetValues(typeof(Locales)))
            {
                Section variant = _context.Sections.Where(s => s.GroupId == groupId && s.Lang == (Locales)lang).FirstOrDefault();
                variants.Add(lang.ToString(), variant);
            }

            return variants;
        }

        private int _GetGroupId(Section section)
        {
            _context.Add(section);
            _context.SaveChanges();
            return section.Id;
        }
    }
}
