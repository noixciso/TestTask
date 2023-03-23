using System.Collections.Generic;
using CodeBase.Items;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerBag : MonoBehaviour
    {
        [Range(1, 10)]
        public int BagCapacity;
        public Transform BagTransform;

        private List<Item> _bag = new List<Item>();

        public List<Item> Bag => _bag;
   
        public void DropItem(Item item, Transform consumedWarehouseTransform)
        {
            if (_bag != null)
            {
                _bag.Remove(item);
                StartCoroutine(item.DropAnimation(BagTransform, consumedWarehouseTransform));
            }
        }

        public bool TryPickUp(Item item)
        {
            if (BagCapacity > _bag.Count)
            {
                _bag.Add(item);
                StartCoroutine(item.PickUpAnimation(BagTransform));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}