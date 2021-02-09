using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class PlaceObjectButton : MonoBehaviour
    {
        [SerializeField]
        PlacementMenu placementMenu;

        Equipment targetEqupment;

        private void Start()
        {
            DragHandler.singleton.onSelectedEquipment.AddListener(OnSelectedEquipment);
            DragHandler.singleton.onUnselectedEquipment.AddListener(OnUnselectedEquipment);
        }

        public void PlaceObject()
        {
            if (targetEqupment.CanBePlacedWithNoHolder())
                targetEqupment.FixPosition();
            else
            {
                placementMenu.ShowMenu(targetEqupment);
            }
            DragHandler.singleton.UnselectEquipment();
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