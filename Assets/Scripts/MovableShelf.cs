using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class MovableShelf : MonoBehaviour
    {
        [SerializeField]
        EquipmentContainer upperContainer;
        [SerializeField]
        EquipmentContainer myContainer;
        [SerializeField]
        EquipmentContainer lowerContainer;

        Rigidbody rigidbody;
        
        Vector3 targetPosition;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            targetPosition = transform.position;
        }

        private void Start()
        {
            RecalculateVerticalCapacities();
        }

        private void FixedUpdate()
        {
            if(!rigidbody.isKinematic)
                rigidbody.MovePosition(targetPosition);
        }

        public void StartMoving()
        {
            rigidbody.isKinematic = false;
        }

        public void StopMoving()
        {
            rigidbody.isKinematic = true;
            RecalculateVerticalCapacities();
        }

        public void Move(float direction)
        {
            targetPosition = transform.position;
            targetPosition.y = direction;
            float maxHeight = GetUpperLimit();
            float minHeight = GetLowerLimit();
            targetPosition.y = Mathf.Clamp(targetPosition.y, minHeight, maxHeight);
        }

        private void RecalculateVerticalCapacities()
        {
            float myVerticalCapacity = 0;
            if (upperContainer)
                myVerticalCapacity = upperContainer.transform.position.y - transform.position.y;
            else
                myVerticalCapacity = ShelfPositioningHandler.singleton.MaxHeight - transform.position.y;

            myContainer.SetVerticalCapacity(myVerticalCapacity);

            if (lowerContainer)
            {
                float lowerContainerVerticalCapacity = transform.position.y - lowerContainer.transform.position.y;
                lowerContainer.SetVerticalCapacity(lowerContainerVerticalCapacity);
            }
        }

        private float GetUpperLimit()
        {
            if (upperContainer)
                return upperContainer.transform.position.y - myContainer.HeightOccupied;
            else
                return ShelfPositioningHandler.singleton.MaxHeight - myContainer.HeightOccupied;
        }

        private float GetLowerLimit()
        {
            if (lowerContainer)
                return lowerContainer.transform.position.y + lowerContainer.HeightOccupied;
            else
                return ShelfPositioningHandler.singleton.MinHeight;
        }
    }
}