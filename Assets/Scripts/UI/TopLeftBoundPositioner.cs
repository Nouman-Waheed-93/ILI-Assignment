using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class TopLeftBoundPositioner : MonoBehaviour
    {
        SpaceOccupier target;

        [SerializeField]
        Vector3 positionOffset;

        private void Start()
        {
            DragHandler.singleton.onSelectedObject.AddListener(OnSelectedObject);
            DragHandler.singleton.onUnselectedObject.AddListener(OnUnselectedObject);
        }

        void Update()
        {
            if (target)
            {
                Vector3 newPosition = new Vector3(target.BoundingBox.center.x, target.BoundingBox.max.y, target.BoundingBox.center.z); 
                transform.position = newPosition + positionOffset;
            }
        }
        
        private void OnSelectedObject(GameObject selectedGameObject)
        {
            selectedGameObject.TryGetComponent<SpaceOccupier>(out target);
        }

        private void OnUnselectedObject(GameObject unselectedGameObject)
        {
            target = null;
        }
    }
}
