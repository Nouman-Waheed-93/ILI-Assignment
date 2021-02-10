using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class BoundingBoxRenderer : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer targetObject;
        LineRenderer[] lines = new LineRenderer[12];

        Vector3 topFrontLeft;
        Vector3 topFrontRight;
        Vector3 topBackLeft;
        Vector3 topBackRight;
        Vector3 bottomFrontLeft;
        Vector3 bottomFrontRight;
        Vector3 bottomBackLeft;
        Vector3 bottomBackRight;
        
        private void Awake()
        {
            for (int i = 0; i < 12; i++)
            {
                GameObject lineObject = new GameObject();
                lineObject.transform.parent = transform;
                lines[i] = lineObject.AddComponent<LineRenderer>();
                lines[i].useWorldSpace = false;
                lines[i].positionCount = 2;
            }

            Bounds bounds = targetObject.bounds;
            CalculateEdges(bounds);
            SetLinePositions();
        }

        public void Hide()
        {
            foreach(LineRenderer line in lines)
            {
                line.enabled = false;
            }
        }

        public void SetRightMaterial()
        {
            ChangeMaterial(BoundingBoxMaterialHandler.singleton.RightPositionMaterial);
        }

        public void SetWrongMaterial()
        {
            ChangeMaterial(BoundingBoxMaterialHandler.singleton.WrongPositionMaterial);
        }

        private void ChangeMaterial(Material material)
        {
            if (lines[0] != material)
            {
                foreach (LineRenderer line in lines)
                {
                    line.material = material;
                }
            }
        }

        private void CalculateEdges(Bounds bounds)
        {
            topFrontRight = transform.TransformPoint(bounds.extents);
            topFrontLeft = transform.TransformPoint(Vector3.Scale(bounds.extents, new Vector3(-1, 1, 1)));
            topBackRight = transform.TransformPoint(Vector3.Scale(bounds.extents, new Vector3(1, 1, -1)));
            topBackLeft = transform.TransformPoint(Vector3.Scale(bounds.extents, new Vector3(-1, 1, -1)));
            bottomFrontRight = transform.TransformPoint(Vector3.Scale(bounds.extents, new Vector3(1, -1, 1)));
            bottomFrontLeft = transform.TransformPoint(Vector3.Scale(bounds.extents, new Vector3(-1, -1, 1)));
            bottomBackRight = transform.TransformPoint(Vector3.Scale(bounds.extents, new Vector3(1, -1, -1)));
            bottomBackLeft = transform.TransformPoint(Vector3.Scale(bounds.extents, new Vector3(-1, -1, -1)));
        }

        private void SetLinePositions()
        {
            lines[0].SetPosition(0, topFrontLeft);
            lines[0].SetPosition(1, topFrontRight);

            lines[1].SetPosition(0, bottomFrontLeft);
            lines[1].SetPosition(1, bottomFrontRight);

            lines[2].SetPosition(0, topBackLeft);
            lines[2].SetPosition(1, topBackRight);

            lines[3].SetPosition(0, bottomBackLeft);
            lines[3].SetPosition(1, bottomBackRight);

            lines[4].SetPosition(0, topFrontLeft);
            lines[4].SetPosition(1, topBackLeft);

            lines[5].SetPosition(0, topFrontRight);
            lines[5].SetPosition(1, topBackRight);

            lines[6].SetPosition(0, bottomFrontLeft);
            lines[6].SetPosition(1, bottomBackLeft);

            lines[7].SetPosition(0, bottomFrontRight);
            lines[7].SetPosition(1, bottomBackRight);

            lines[8].SetPosition(0, topFrontLeft);
            lines[8].SetPosition(1, bottomFrontLeft);

            lines[9].SetPosition(0, topFrontRight);
            lines[9].SetPosition(1, bottomFrontRight);

            lines[10].SetPosition(0, topBackLeft);
            lines[10].SetPosition(1, bottomBackLeft);

            lines[11].SetPosition(0, topBackRight);
            lines[11].SetPosition(1, bottomBackRight);
        }
    }
}