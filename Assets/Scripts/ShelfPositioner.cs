using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ShelfPositioner : MonoBehaviour
    {
        [SerializeField]
        MovableShelf shelf;
        
        public void MouseDown()
        {
            shelf.StartMoving();
        }

        public void MouseUp()
        {
            shelf.StopMoving();
        }

        public void MouseDrag()
        {
            shelf.Move(DragHandler.singleton.GetWorldPointOnObjectPlane(transform).y);
        }
    }
}
