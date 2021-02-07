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
        #endregion

        public void Initialize(EquipmentContainer container)
        {
            currentContainer = container;
            currentContainer.OccupySpace(spaceOccupier);
        }

        public void Move(Vector3 position, EquipmentContainer container)
        {
            Vector3 newPosition = position + new Vector3(0, spaceOccupier.Volume.y * 0.5f, 0);
            if (OverlapsOtherEquipment(newPosition, container)
                || !PositionIsInsideContainer(newPosition, container))
                return;

            if(container != currentContainer)
            {
                currentContainer.UnoccupySpace(spaceOccupier);
                currentContainer = container;
                currentContainer.OccupySpace(spaceOccupier);
            }
            transform.position = position + new Vector3(0, spaceOccupier.Volume.y * 0.5f, 0);
        }

        private bool OverlapsOtherEquipment(Vector3 newPosition, EquipmentContainer container)
        {
            Collider[] colliders = Physics.OverlapBox(newPosition, spaceOccupier.Volume * 0.5f, transform.rotation, DragHandler.singleton.equipmentLayer);

            bool overlaps = true;

            if (colliders.Length == 1)
                overlaps = colliders[0].gameObject != gameObject;

            if (colliders.Length == 0)
                overlaps = false;

            return overlaps ;
        }

        private bool PositionIsInsideContainer(Vector3 newPosition, EquipmentContainer container)
        {
            bool isInside = false;

            Vector3 containerCenter = container.transform.position + new Vector3(0, container.Capacity.y * 0.5f, 0);
            Bounds containerBounds = new Bounds(containerCenter, container.Capacity);

            Vector3 maxEquipmentBound = newPosition + spaceOccupier.Volume * 0.5f;
            Vector3 minEquipmentBound = newPosition - spaceOccupier.Volume * 0.5f;

            isInside = containerBounds.Contains(maxEquipmentBound) && containerBounds.Contains(minEquipmentBound);
            return isInside;
        }
    }
}