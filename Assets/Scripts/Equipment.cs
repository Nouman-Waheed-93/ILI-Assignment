using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class Equipment : MonoBehaviour
    {
        public bool isPlaced { get; private set; }
        public EquipmentContainer currentContainer { get; private set; }
        [SerializeField]
        List<PlacingMethod> placingMethods; //the methods with which this equipment can be placed in a container

        SpaceOccupier spaceOccupier;
        BoundingBoxRenderer boundingBoxRenderer;

        List<PlacingMethod> commonPlacingMethodsWithContainer = new List<PlacingMethod>();
        #region Unity Callbacks
        private void Awake()
        {
            spaceOccupier = GetComponent<SpaceOccupier>();
            boundingBoxRenderer = GetComponent<BoundingBoxRenderer>();
            if (currentContainer)
                currentContainer.OccupySpace(spaceOccupier);
        }
        #endregion

        #region Public Methods
        public void Initialize(EquipmentContainer container)
        {
            currentContainer = container;
            currentContainer.OccupySpace(spaceOccupier);
            SetCommonPlacingMethods(container);
            if(Utility.OverlapsOtherEquipment(spaceOccupier, transform.position, transform.rotation, container))
            {
                boundingBoxRenderer.SetWrongMaterial();
            }
            else
            {
                boundingBoxRenderer.SetRightMaterial();
            }
        }

        public bool CanBePlacedWithNoHolder()
        {
            return commonPlacingMethodsWithContainer.Count == 1
                && commonPlacingMethodsWithContainer[0] == PlacingMethod.SelfContained;
        }

        public PlacingMethod[] GetAvailablePlacingMethods()
        {
            PlacingMethod[] placingMethods = new PlacingMethod[commonPlacingMethodsWithContainer.Count];
            commonPlacingMethodsWithContainer.CopyTo(placingMethods);
            return placingMethods;
        }

        public void FixPosition()
        {
            isPlaced = true;
        }

        public void Rotate(Quaternion rotation)
        {
            if (isPlaced)
                return;
            
            if (Utility.OverlapsOtherEquipment(spaceOccupier, transform.position, rotation, currentContainer))
                return;
            
            if(IsRotationInsideContainer(rotation))
                transform.rotation = rotation;
        }

        public void Move(Vector3 position, EquipmentContainer container)
        {
            if (isPlaced)
                return;

            Vector3 newPosition = position + new Vector3(0, spaceOccupier.Volume.y * 0.5f, 0);
            newPosition = Utility.KeepPositionInsideContainer(spaceOccupier, newPosition, container);

            if (Utility.OverlapsOtherEquipment(spaceOccupier, newPosition, transform.rotation, container))
                return;

            if (container.Capacity.y < spaceOccupier.Volume.y)
                return;

            if (container != currentContainer)
            {
                if (!CanBePlacedInContainer(container))
                    return;

                SetCommonPlacingMethods(container);
                currentContainer.UnoccupySpace(spaceOccupier);
                currentContainer = container;
                currentContainer.OccupySpace(spaceOccupier);
                transform.rotation = container.transform.rotation;
            }
            boundingBoxRenderer.SetRightMaterial();
            transform.position = newPosition;
        }

        #endregion

        #region Private Methods

        private void SetCommonPlacingMethods(EquipmentContainer container)
        {
            commonPlacingMethodsWithContainer.Clear();
            foreach (PlacingMethod placingMethod in placingMethods)
            {
                if (container.IsPlacingMethodSupported(placingMethod))
                    commonPlacingMethodsWithContainer.Add(placingMethod);
            }
        }

        private bool CanBePlacedInContainer(EquipmentContainer container)
        {
            foreach (PlacingMethod placingMethod in placingMethods)
            {
                if (container.IsPlacingMethodSupported(placingMethod))
                    return true;
            }
            return false;
        }

        private bool IsRotationInsideContainer(Quaternion rotation)
        {
            Quaternion previousRotation = transform.rotation;
            transform.rotation = rotation;

            Vector3 containerCenter = currentContainer.transform.position + new Vector3(0, currentContainer.Capacity.y * 0.5f, 0);

            if (currentContainer.containerType == EquipmentContainer.ContainerType.shelf)
                containerCenter = currentContainer.transform.position + Vector3.up * currentContainer.Capacity.y;
            else
                containerCenter = currentContainer.transform.position + currentContainer.transform.up * currentContainer.Capacity.z;

            Bounds containerBounds = new Bounds(containerCenter, currentContainer.Capacity);
            Vector3 halfVolume = spaceOccupier.BoundingBox.extents;
            Vector3 maxEquipmentBound = transform.position + halfVolume;
            Vector3 minEquipmentBound = transform.position - halfVolume;

            transform.rotation = previousRotation;

            bool contains = containerBounds.max.x >= maxEquipmentBound.x
                && containerBounds.min.x <= minEquipmentBound.x;

            switch (currentContainer.containerType)
            {
                case EquipmentContainer.ContainerType.shelf:
                    {
                        contains &= containerBounds.max.z >= maxEquipmentBound.z
                        && containerBounds.min.z <= minEquipmentBound.z;
                        break;
                    }
                case EquipmentContainer.ContainerType.backWall:
                    {
                        contains &= containerBounds.max.y >= maxEquipmentBound.y
                        && containerBounds.min.y <= minEquipmentBound.y;
                        break;
                    }
            }
            return contains;
        }

        #endregion
    }
}