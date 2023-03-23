using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Items
{
    public class Item : MonoBehaviour
    {
        public ItemTypeId TypeId;
        
        private float _offset = 0.2f;

        public IEnumerator SpawnAnimation(Transform startPositionTransform, Transform productionWarehouseTransform)
        {
            float elapsedTime = 0f;
            float waitTime = 1f;
            Vector3 newPosition = new Vector3(productionWarehouseTransform.localPosition.x, _offset + productionWarehouseTransform.childCount);
        
            while (elapsedTime < waitTime)
            {
                transform.localPosition = Vector3.Lerp(startPositionTransform.transform.localPosition, newPosition, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
        
                yield return null;
            }
        
            yield return null;
        }

        public IEnumerator PickUpAnimation(Transform bagTransform)
        {
            float elapsedTime = 0f;
            float waitTime = 1f;
            Vector3 newPosition = new Vector3(bagTransform.transform.localPosition.x, _offset * bagTransform.childCount);
        
        
            transform.SetParent(bagTransform);
        
            while (elapsedTime < waitTime)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, (elapsedTime / waitTime));
                transform.localRotation = Quaternion.identity;
                elapsedTime += Time.deltaTime;
        
                yield return null;
            }
            
            yield return null;
        }

        public IEnumerator DropAnimation(Transform bagTransform, Transform consumedWarehouseTransform)
        {
            float elapsedTime = 0f;
            float waitTime = 1f;
            
            transform.SetParent(consumedWarehouseTransform);
        
            while (elapsedTime < waitTime)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, consumedWarehouseTransform.localPosition, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
        
                yield return null;
            }
        
            StartCoroutine(DeleteAnimation(consumedWarehouseTransform));
            yield return null;
        }

        public IEnumerator DeleteAnimation(Transform consumedWarehouseTransform)
        {
            float elapsedTime = 0f;
            float waitTime = 1f;
            
            transform.SetParent(consumedWarehouseTransform.parent);
        
            while (elapsedTime < waitTime)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, consumedWarehouseTransform.parent.parent.localPosition, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
        
                yield return null;
            }
            
            Destroy(gameObject);
            yield return null;
        }
    }
}