using rtmcuz.Data.Enums;
using rtmcuz.Data.Models;

namespace rtmcuz.Interfaces
{
    public interface ISectionRepository
    {
        List<Section> ListItems(SectionTypes type);
        Task<List<Section>> ListItemsAsync(SectionTypes type);
        void Create(Section section, IFormFile? image = null);
        void Save(Section section, IFormFile? image = null);
        Section GetItem(int? id);
        Task<Section> GetItemAsync(int? id);
        Task DeleteConfirmed(int id);
        bool Exists(int? id, SectionTypes type);
        bool IsNull(SectionTypes type);
        bool Any(int id);
        Dictionary<string, Section> VariantsList(int groupId);
    }
}
