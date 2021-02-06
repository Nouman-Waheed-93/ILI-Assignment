using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class DragHandler : MonoBehaviour
    {
        public static DragHandler singleton;

        [SerializeField]
        Camera camera;
        [SerializeField]
        LayerMask dragLayer;
        public LayerMask equipmentLayer;

        public Equipment activeEquipment;

        private void Awake()
        {
            singleton = this;
        }

        public void Update()
        {
            if (activeEquipment)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    activeEquipment = null;
                    return;
                }
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, int.MaxValue, dragLayer))
                {
                    activeEquipment.Move(hit.point, hit.collider.GetComponentInParent<EquipmentContainer>());
                }
            }
        }
    }
}