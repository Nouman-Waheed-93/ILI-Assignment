using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class RotationArrow : MonoBehaviour
    {
        public static RotationArrow activeArrow;
        [SerializeField]
        RotationGizmo rotator;


        Vector3 lastMousePosition;

        private void OnMouseDown()
        {
            activeArrow = this;
            lastMousePosition = Input.mousePosition;
            rotator.RotationStarted();
        }

        private void OnMouseDrag()
        {
            if ((Input.mousePosition - lastMousePosition).x > 0)
            {
                rotator.Rotate(-1);
            }
            else if ((Input.mousePosition - lastMousePosition).x < 0)
            {
                rotator.Rotate(1);
            }
        }

    }
}