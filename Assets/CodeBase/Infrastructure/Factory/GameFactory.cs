using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Items;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData)
        {
            _assets = assets;
            _staticData = staticData;
        }

        public void CreateHud() =>
            _assets.Instantiate(AssetPath.HudPath);

        public GameObject CreatePlayer(GameObject at) => 
            _assets.Instantiate(AssetPath.PlayerPath, at: at.transform.position);
        
        public Item CreateItem(Item item, Transform parent)
        {
            BuildingStaticData buildingData = _staticData.ForProducedItems(item.TypeId);
            Item newItem = Object.Instantiate(buildingData.ProducedPrefab, parent, true);

            return newItem;
        }
    }
}