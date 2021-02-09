using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireTruckStoreApp
{
    public class PlacementMenu : MonoBehaviour
    {
        [SerializeField]
        Button clipperButton;
        [SerializeField]
        Button boxButton;

        Equipment equipment;
        
        public void ShowMenu(Equipment equipment)
        {
            this.equipment = equipment;
            clipperButton.interactable = false;
            boxButton.interactable = false;

            PlacingMethod[] placingMethods = equipment.GetAvailablePlacingMethods();
            for(int i = 0; i < placingMethods.Length; i++)
            {
                switch (placingMethods[i])
                {
                    case PlacingMethod.Box:
                        {
                            boxButton.interactable = true;
                            break;
                        }
                    case PlacingMethod.Clippers:
                        {
                            clipperButton.interactable = true;
                            break;
                        }
                }
            }
            gameObject.SetActive(true);
        }

        public void PlaceWithClippers()
        {
            equipment.FixPosition();
            gameObject.SetActive(false);
        }
        
        public void PlaceWithBox()
        {
            equipment.FixPosition();
            gameObject.SetActive(false);
        }

    }
}