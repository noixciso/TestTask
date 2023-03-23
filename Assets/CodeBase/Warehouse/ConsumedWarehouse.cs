using System.Collections.Generic;
using System.Linq;
using CodeBase.Items;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Warehouse
{
    [RequireComponent(typeof(ConsumedWarehouseTrigger))]
    public class ConsumedWarehouse : MonoBehaviour
    {
        private List<Item> _requiredItemsForProduction = new List<Item>();

        public IReadOnlyCollection<Item> RequiredItemsForProduction => _requiredItemsForProduction;
        public ConsumedWarehouseTrigger WarehouseTrigger { get; private set; }

        private void Start() => 
            WarehouseTrigger = GetComponent<ConsumedWarehouseTrigger>();

        public void AddItemToRequired(Item item) => 
            _requiredItemsForProduction.Add(item);

        public bool IsProductionPossible(BuildingStaticData buildingStaticData)
        {
            bool AllFound = false;
            List<bool> isFound = new List<bool>();

            for (int index = 0; index < buildingStaticData.RequiredTypeId.Length; index++)
            {
                if (IsEnoughItemsWithThisTypeId(buildingStaticData, index))
                {
                    isFound.Add(true);
                }
                else
                {
                    isFound.Add(false);
                }
            }
            
            if (IsAllRequiredItemsFound(isFound))
            {
                AllFound = true;
            }

            isFound.Clear();

            return AllFound;
        }

        public void RemoveRequiredItems(BuildingStaticData buildingStaticData)
        {
            IEnumerable<Item> itemsForRemoval;
            
            for (var index = 0; index < buildingStaticData.RequiredTypeId.Length; index++)
            {
                itemsForRemoval = _requiredItemsForProduction
                    .FindAll(item => item.TypeId == buildingStaticData.RequiredTypeId[index])
                    .Take(buildingStaticData.RequiredAmountEachElement[index]);

                foreach (Item item in itemsForRemoval)
                {
                    _requiredItemsForProduction.Remove(item);
                }
                
            }
            
        }

        private static bool IsAllRequiredItemsFound(List<bool> isFound)
        {
            return isFound.All(item => item.Equals(true));
        }

        private bool IsEnoughItemsWithThisTypeId(BuildingStaticData buildingStaticData, int index)
        {
            return _requiredItemsForProduction.FindAll(item => buildingStaticData.RequiredTypeId[index] == item.TypeId).Count >= buildingStaticData.RequiredAmountEachElement[index];
        }
    }
}