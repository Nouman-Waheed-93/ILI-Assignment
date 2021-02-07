using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class SpaceOccupier : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer renderer;

        [SerializeField]
        private Vector3 volume;

        public Vector3 Volume { get { return volume;} }
        public Vector3 BoundingBox { get { return renderer.bounds.size; } }
    }
}