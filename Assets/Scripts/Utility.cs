using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public static class Utility
    {

        public static bool OverlapsOtherEquipment(SpaceOccupier spaceOccupier, Vector3 newPosition, Quaternion newRotation, EquipmentContainer container)
        {
            Collider[] colliders = Physics.OverlapBox(newPosition, spaceOccupier.Volume * 0.5f, newRotation, DragHandler.singleton.equipmentLayer);

            bool overlaps = true;

            if (colliders.Length == 1)
                overlaps = colliders[0].gameObject != spaceOccupier.gameObject;

            if (colliders.Length == 0)
                overlaps = false;

            return overlaps;
        }

        public static Vector3 KeepPositionInsideContainer(SpaceOccupier spaceOccupier, Vector3 newPosition, EquipmentContainer container)
        {
            Vector3 clampedPosition = newPosition;
            Vector3 containerCenter = Vector3.zero;

            if (container.containerType == EquipmentContainer.ContainerType.shelf)
                containerCenter = container.transform.position + Vector3.up * container.Capacity.y;
            else
                containerCenter = container.transform.position + container.transform.up * container.Capacity.z;

            Bounds containerBounds = new Bounds(containerCenter, container.Capacity);

            Vector3 halfVolume = spaceOccupier.BoundingBox.extents;

            Vector3 maxEquipmentBound = newPosition + halfVolume;
            Vector3 minEquipmentBound = newPosition - halfVolume;

            if (containerBounds.max.x < maxEquipmentBound.x)
                clampedPosition.x = containerBounds.max.x - halfVolume.x;

            if (containerBounds.min.x > minEquipmentBound.x)
                clampedPosition.x = containerBounds.min.x + halfVolume.x;

            switch (container.containerType)
            {
                case EquipmentContainer.ContainerType.shelf:
                    {
                        if (containerBounds.max.z < maxEquipmentBound.z)
                            clampedPosition.z = containerBounds.max.z - halfVolume.z;

                        if (containerBounds.min.z > minEquipmentBound.z)
                            clampedPosition.z = containerBounds.min.z + halfVolume.z;
                        break;
                    }
                case EquipmentContainer.ContainerType.backWall:
                    {
                        if (containerBounds.max.y < maxEquipmentBound.y)
                            clampedPosition.y = containerBounds.max.y - halfVolume.y;

                        if (containerBounds.min.y > minEquipmentBound.y)
                            clampedPosition.y = containerBounds.min.y + halfVolume.y;
                        break;
                    }
            }
            return clampedPosition;
        }

    }
}