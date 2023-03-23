using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        public Transform ConsumedWarehousePoint;
        public Transform ProductionWarehousePoint;

        [SerializeField] private BuildingStaticData _buildingStaticData;
        [SerializeField] private GameObject StartSpawnPoint;

        private IGameFactory _factory;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public Item Spawn()
        {
            Item item = _factory.CreateItem(_buildingStaticData.ProducedPrefab, ProductionWarehousePoint);
            StartCoroutine(item.SpawnAnimation(StartSpawnPoint.transform, ProductionWarehousePoint));
            return item;
        }
    }
}