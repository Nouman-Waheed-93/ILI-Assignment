using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class EquipmentContainer : MonoBehaviour
    {
        [SerializeField]
        Vector3 capacity;

        public Vector3 Capacity { get { return capacity; } }
        public float HeightOccupied { get; private set; }

        List<SpaceOccupier> containedEquipment = new List<SpaceOccupier>();

        public void OccupySpace(SpaceOccupier occupier)
        {
            if (occupier.Volume.y > HeightOccupied)
                HeightOccupied = occupier.Volume.y;
            containedEquipment.Add(occupier);
            occupier.transform.parent = transform;
        }

        public void UnoccupySpace(SpaceOccupier occupier)
        {
            containedEquipment.Remove(occupier);
            if (HeightOccupied == occupier.Volume.y)
                ResetHeight();
        }

        public void SetVerticalCapacity(float verticalCapacity)
        {
            capacity.y = verticalCapacity;
        }

        private void ResetHeight()
        {
            float maxHeight = 0;
            foreach(SpaceOccupier equipment in containedEquipment)
            {
                if (equipment.Volume.y > maxHeight)
                    maxHeight = equipment.Volume.y;
            }
            HeightOccupied = maxHeight;
        }
    }
}