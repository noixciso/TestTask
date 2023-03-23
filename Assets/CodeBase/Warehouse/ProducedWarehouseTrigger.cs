using System;
using System.Linq;
using CodeBase.Player;
using UnityEngine;

namespace CodeBase.Warehouse
{
    [RequireComponent(typeof(ProducedWarehouse))]
    public class ProducedWarehouseTrigger : MonoBehaviour
    {
        private ProducedWarehouse _producedWarehouse;
        private float delay = 1;
        private float timer;

        private void Start()
        {
            _producedWarehouse = GetComponent<ProducedWarehouse>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out PlayerBag playerBag))
            {
                timer += Time.deltaTime;

                if (_producedWarehouse._producedItems.Count > 0)
                {
                    if (timer > delay)
                    {
                        if (playerBag.TryPickUp(_producedWarehouse._producedItems.Last()))
                        {
                            _producedWarehouse._producedItems.Remove(_producedWarehouse._producedItems.Last());
                        }

                        timer -= delay;
                    }
                }
            }
        }
    }
}