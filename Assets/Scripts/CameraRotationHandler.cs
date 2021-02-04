using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class CameraRotationHandler : MonoBehaviour
    {
        [SerializeField]
        Transform camera;
        [SerializeField]
        float unitRotation = 1;
        [SerializeField]
        float maxUpRotation;
        [SerializeField]
        float maxDownRotation;
        [SerializeField]
        float maxLeftRotation;
        [SerializeField]
        float maxRightRotation;
        
        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
            angleX = Mathf.Clamp(angleX, maxDownRotation, maxUpRotation);
            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
        
        public void RotateUpwards()
        {
            Quaternion quaternion = camera.rotation;
            quaternion *= Quaternion.Euler(unitRotation, 0, 0f);
            camera.rotation = ClampRotationAroundXAxis(quaternion);
        }

        public void RotateDownwards()
        {
            Quaternion quaternion = camera.rotation;
            quaternion *= Quaternion.Euler(-unitRotation, 0, 0f);
            camera.rotation = ClampRotationAroundXAxis(quaternion);
        }

        public void RotateLeft()
        {
            Quaternion quaternion = camera.rotation;
            quaternion *= Quaternion.Euler(0f, -unitRotation, 0f);
            camera.rotation = quaternion;
        }

        public void RotateRight()
        {
            Quaternion quaternion = camera.rotation;
            quaternion *= Quaternion.Euler(0f, unitRotation, 0f);
            camera.rotation = quaternion;
        }
    }
}