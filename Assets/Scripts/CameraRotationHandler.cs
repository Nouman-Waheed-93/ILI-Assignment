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



        //private Quaternion m_CharacterTargetRot;
        //private Quaternion m_CameraTargetRot;

        //public void Init(Transform character, Transform camera)
        //{
        //    m_CharacterTargetRot = character.localRotation;
        //    m_CameraTargetRot = camera.localRotation;
        //}

        //public void LookRotation(Transform character, Transform camera)
        //{
        //    float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
        //    float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

        //    m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        //    m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        //    if (clampVerticalRotation)
        //        m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        //    if (smooth)
        //    {
        //        character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot, smoothTime * Time.deltaTime);
        //        camera.localRotation = Quaternion.Slerp(camera.localRotation, m_CameraTargetRot, smoothTime * Time.deltaTime);
        //    }
        //    else
        //    {
        //        character.localRotation = m_CharacterTargetRot;
        //        camera.localRotation = m_CameraTargetRot;
        //    }
        //}

        //Quaternion ClampRotationAroundXAxis(Quaternion q)
        //{
        //    q.x /= q.w;
        //    q.y /= q.w;
        //    q.z /= q.w;
        //    q.w = 1.0f;

        //    float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        //    angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
        //    q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        //    return q;
        //}




        public void RotateUpwards()
        {
            Vector3 rotation = camera.eulerAngles;
            rotation.x += unitRotation;
            if (rotation.x > maxUpRotation)
                rotation.x = maxUpRotation;
            camera.rotation = Quaternion.Euler(rotation);
        }

        public void RotateDownwards()
        {
            Vector3 rotation = camera.eulerAngles;
            rotation.x -= unitRotation;
            if (rotation.x < maxDownRotation)
                rotation.x = maxDownRotation;
            camera.rotation = Quaternion.Euler(rotation);
        }

        public void RotateLeft()
        {
            
            Quaternion quaternion = camera.rotation;
            quaternion *= Quaternion.Euler(0f, -unitRotation, 0f);

//            Vector3 rotation = camera.eulerAngles;
//            rotation.y -= unitRotation;
//            if (rotation.y < maxLeftRotation)
//                rotation.y = maxLeftRotation;
            camera.rotation = quaternion;
        }

        public void RotateRight()
        {
            Vector3 rotation = camera.eulerAngles;
            rotation.y += unitRotation;
            if (rotation.y > maxRightRotation)
                rotation.y = maxRightRotation;
            camera.rotation = Quaternion.Euler(rotation);
        }
    }
}