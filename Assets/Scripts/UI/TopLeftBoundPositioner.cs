using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class TopLeftBoundPositioner : MonoBehaviour
    {
        [SerializeField]
        Vector3 positionOffset;
        [SerializeField]
        bool activeForPlacedObject;

        SpaceOccupier target;

        private void Start()
        {
            DragHandler.singleton.onSelectedEquipment.AddListener(OnTargetSelected);
            DragHandler.singleton.onUnselectedEquipment.AddListener(OnTargetUnselected);
        }
        
        void Update()
        {
            if (target)
            {
                Vector3 newPosition = new Vector3(target.BoundingBox.center.x, target.BoundingBox.max.y, target.BoundingBox.center.z); 
                transform.position = newPosition + positionOffset;
            }
        }
        
        private void OnTargetSelected(Equipment equipment)
        {
            if (!equipment.isPlaced || activeForPlacedObject)
            {
                target = equipment.GetComponent<SpaceOccupier>();
                gameObject.SetActive(true);
            }
        }

        private void OnTargetUnselected(Equipment equipment)
        {
            target = null;
            gameObject.SetActive(false);
        }
    }
}
