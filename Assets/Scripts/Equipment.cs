using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class Equipment : MonoBehaviour
    {
        SpaceOccupier spaceOccupier;
        EquipmentContainer currentContainer;

        #region Unity Callbacks
        private void Awake()
        {
            spaceOccupier = GetComponent<SpaceOccupier>();
            if (currentContainer)
                currentContainer.OccupySpace(spaceOccupier);
        }

        private void OnMouseDown()
        {
            DragHandler.singleton.activeEquipment = this;
        }
        #endregion

        public void Initialize(EquipmentContainer container)
        {
            currentContainer = container;
        }

        public void Move(Vector3 position, EquipmentContainer container)
        {
            Vector3 newPosition = position + new Vector3(0, spaceOccupier.Volume.y * 0.5f, 0);
            Collider[] colliders = Physics.OverlapBox(newPosition, spaceOccupier.Volume * 0.5f, transform.rotation, DragHandler.singleton.equipmentLayer);
            bool canMove = false;

            if (colliders.Length == 1)
            {
                canMove = colliders[0].gameObject == gameObject;
            }
            if (colliders.Length == 0)
                canMove = true;

            if (!canMove)
                return;

            if(container != currentContainer)
            {
                transform.parent = container.transform;
                currentContainer.UnoccupySpace(spaceOccupier);
                currentContainer = container;
                currentContainer.OccupySpace(spaceOccupier);
            }
            transform.position = position + new Vector3(0, spaceOccupier.Volume.y * 0.5f, 0);
        }

    }
}