using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class BoundingBoxMaterialHandler : MonoBehaviour
    {
        public static BoundingBoxMaterialHandler singleton;

        [SerializeField]
        Material wrongPositionMaterial;
        [SerializeField]
        Material rightPositionMaterial;

        private void Awake()
        {
            singleton = this;
        }

        public Material WrongPositionMaterial
        {
            get
            {
                return wrongPositionMaterial;
            }
        }

        public Material RightPositionMaterial
        {
            get
            {
                return rightPositionMaterial;
            }
        }
    }
}