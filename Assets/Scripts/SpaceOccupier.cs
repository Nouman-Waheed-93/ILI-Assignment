using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class SpaceOccupier : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer renderer;
        
        public Vector3 Volume { get { return renderer.bounds.size; } }
        
    }
}