using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ShelfDragActivator : MonoBehaviour
    {
        [SerializeField]
        ShelfPositioner[] positioners;

        public void ToggleDraggers()
        {
            bool enable = !positioners[0].gameObject.activeSelf;
            foreach(ShelfPositioner positioner in positioners)
            {
                positioner.gameObject.SetActive(enable);
            }
        }
    }
}