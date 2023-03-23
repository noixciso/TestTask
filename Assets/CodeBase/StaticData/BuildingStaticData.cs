using CodeBase.Items;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "StaticData/Buildings")]
    public class BuildingStaticData : ScriptableObject
    {
        [Header("Consumed Resources")]
        public ItemTypeId[] RequiredTypeId;

        [Range(0,10)]
        public int[] RequiredAmountEachElement;


        [Header("Produced Resources")]
        [Range(0,10)]
        public int CapacityProduced;
        public ItemTypeId ProducedTypeId;
        public Item ProducedPrefab;
    }
}
