using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ShelfPositioningHandler : MonoBehaviour
    {
        public static ShelfPositioningHandler singleton;

        bool positioningEnabled;

        public float MaxHeight { get { return maxHeightIndicator.position.y; } }
        public float MinHeight { get { return minHeightIndicator.position.y; } }

        [SerializeField]
        Transform maxHeightIndicator;
        [SerializeField]
        Transform minHeightIndicator;

        [SerializeField]
        ShelfPositioner[] shelfPositioners;

        private void Awake()
        {
            singleton = this;
        }

        public void TogglePositioning()
        {
            positioningEnabled = !positioningEnabled;
            foreach(ShelfPositioner positioner in shelfPositioners)
            {
                positioner.gameObject.SetActive(positioningEnabled);
            }
        }
        
    }
}