using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ShelfPositioner : MonoBehaviour
    {
        [SerializeField]
        MovableShelf shelf;

        Vector3 lastMousePosition;

        public void MouseDown()
        {
            shelf.StartMoving();
            lastMousePosition = Input.mousePosition;
        }

        public void MouseUp()
        {
            shelf.StopMoving();
        }

        public void MouseDrag()
        {
            shelf.Move((Input.mousePosition - lastMousePosition).y * Time.deltaTime);
        }
    }
}
