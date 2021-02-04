using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class CameraZoomHandler : MonoBehaviour
    {
        [SerializeField]
        Camera camera;
        [SerializeField]
        float zoomStep;
        [SerializeField]
        float minFOV;
        [SerializeField]
        float maxFOV;

        public void ZoomIn()
        {
            float FOV = camera.fieldOfView;
            FOV -= zoomStep;
            if (FOV < minFOV)
                FOV = minFOV;
            camera.fieldOfView = FOV;
        }

        public void ZoomOut()
        {
            float FOV = camera.fieldOfView;
            FOV += zoomStep;
            if (FOV > maxFOV)
                FOV = maxFOV;
            camera.fieldOfView = FOV;
        }
    }
}
