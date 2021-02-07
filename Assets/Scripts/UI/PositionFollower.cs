using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class PositionFollower : MonoBehaviour
    {
        public Transform target;

        [SerializeField]
        Vector3 positionOffset;

        private void Update()
        {
            if(target)
                transform.position = target.position + positionOffset;
        }
    }
}