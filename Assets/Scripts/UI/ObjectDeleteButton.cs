using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ObjectDeleteButton : MonoBehaviour
    {
        Equipment targetEqupment;

        private void Start()
        {
            DragHandler.singleton.onSelectedEquipment.AddListener(OnSelectedEquipment);
            DragHandler.singleton.onUnselectedEquipment.AddListener(OnUnselectedEquipment);
        }

        public void DeleteTarget()
        {
            targetEqupment.Delete();
        }

        private void OnSelectedEquipment(Equipment equipment)
        {
            targetEqupment = equipment;
        }

        private void OnUnselectedEquipment(Equipment equipment)
        {
            targetEqupment = null;
        }
    }
}