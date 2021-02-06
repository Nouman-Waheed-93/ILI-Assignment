using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class PositionFollower : MonoBehaviour
    {
        [SerializeField]
        Transform target;
        
        private void Update()
        {
            transform.position = target.position;
        }
    }
}