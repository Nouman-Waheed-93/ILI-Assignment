using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireTruckStoreApp
{
    public class PlaceObjectButton : MonoBehaviour
    {
        [SerializeField]
        PlacementMenu placementMenu;

        Button button;

        Equipment targetEqupment;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            DragHandler.singleton.onSelectedObject.AddListener(OnSelectedEquipment);
            DragHandler.singleton.onUnselectedObject.AddListener(OnUnselectedEquipment);
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

        private void OnSelectedEquipment(GameObject selectedGameObject)
        {
            Equipment equipment = selectedGameObject.GetComponent<Equipment>();
            if (equipment)
            {
                targetEqupment = equipment;
                gameObject.SetActive(true);
                button.interactable = !targetEqupment.isPlaced;
            }
        }

        private void OnUnselectedEquipment(GameObject unselectedGameObject)
        {
            targetEqupment = null;
            gameObject.SetActive(false);
        }
    }
}