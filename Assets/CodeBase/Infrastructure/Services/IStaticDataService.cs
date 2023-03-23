using CodeBase.Items;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services
{
    public interface IStaticDataService : IService
    {
        void LoadResources();
        BuildingStaticData ForProducedItems(ItemTypeId typeId);
    }
}