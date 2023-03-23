using System.Collections.Generic;
using CodeBase.Items;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Warehouse
{
    public class ProducedWarehouse : MonoBehaviour
    {
        public List<Item> _producedItems = new List<Item>();

        public IReadOnlyList<Item> ProducedItems => _producedItems;

        public void AddItemToProduced(Item item) => 
            _producedItems.Add(item);

        public bool IsProducedWarehouseFull(BuildingStaticData buildingStaticData) => 
            _producedItems.Count == buildingStaticData.CapacityProduced;

    }
}