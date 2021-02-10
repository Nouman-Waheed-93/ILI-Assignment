using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireTruckStoreApp
{
    public class ObjectDeleteButton : MonoBehaviour
    {
        DeleteableGameObject targetDeleteable;
        Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void Start()
        {
            button = GetComponent<Button>();
            DragHandler.singleton.onSelectedObject.AddListener(OnSelectedObject);
            DragHandler.singleton.onUnselectedObject.AddListener(OnUnselectedObject);
        }

        public void DeleteTarget()
        {
            targetDeleteable.Delete();
        }

        private void OnSelectedObject(GameObject selectedGameObject)
        {
            if (selectedGameObject.TryGetComponent<DeleteableGameObject>(out targetDeleteable))
            {
                gameObject.SetActive(true);
                button.interactable = true;
            }
        }

        private void OnUnselectedObject(GameObject unselectedGameObject)
        {
            targetDeleteable = null;
            gameObject.SetActive(false);
        }
    }
}