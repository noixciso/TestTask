using System.Linq;
using CodeBase.Items;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Warehouse
{
    [RequireComponent(typeof(ConsumedWarehouse))]
    public class ConsumedWarehouseTrigger : MonoBehaviour
    {
        private ConsumedWarehouse _consumedWarehouse;
        private float delay = 1;
        private float timer;

        public bool IsPlayerNotInConsumedWarehouse { get; private set; }

        private void Start()
        {
            _consumedWarehouse = GetComponent<ConsumedWarehouse>();
            IsPlayerNotInConsumedWarehouse = true;
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out PlayerBag playerBag))
            {
                IsPlayerNotInConsumedWarehouse = false;
                
                timer += Time.deltaTime;

                foreach (Item item in playerBag.Bag.ToList())
                {
                    if (timer > delay)
                    {
                        playerBag.DropItem(item, _consumedWarehouse.transform);
                        _consumedWarehouse.AddItemToRequired(item);
                        timer -= delay;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerBag playerBag))
            {
                IsPlayerNotInConsumedWarehouse = true;
            }
        }
    }
}