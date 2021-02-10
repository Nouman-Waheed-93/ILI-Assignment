using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FireTruckStoreApp
{
    public class DragHandler : MonoBehaviour
    {
        public static DragHandler singleton;
        
        public GameObjectEvent onSelectedObject;
        public GameObjectEvent onUnselectedObject;
        public UnityEvent onDragStarted;
        public UnityEvent onDragEnded;

        [SerializeField]
        Camera camera;
        [SerializeField]
        LayerMask dragLayer;
        public LayerMask equipmentLayer;

        Equipment activeEquipment; //the equipment that is being dragged
        GameObject selectedObject; //the equipment that is selected i.e. was last clicked on
        bool dragActive;

        #region Unity Callbacks
        private void Awake()
        {
            singleton = this;
            ActivateDrag();
        }

        public void Update()
        {
            if (!dragActive)
                return;

            if (activeEquipment)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    activeEquipment = null;
                    onDragEnded?.Invoke();
                    return;
                }
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, int.MaxValue, dragLayer))
                {
                    activeEquipment.Move(hit.point, hit.collider.GetComponentInParent<EquipmentContainer>());
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, int.MaxValue, equipmentLayer))
                {
                    activeEquipment = hit.collider.GetComponent<Equipment>();
                    onDragStarted?.Invoke();
                    SelectGameObject(hit.collider.gameObject);
                }
            }
        }
        #endregion

        #region Public Methods
        public void ActivateDrag()
        {
            dragActive = true;
        }

        public void DisactivateDrag()
        {
            dragActive = false;
        }

        public Vector3 GetWorldPointOnObjectPlane(Transform reference)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePoint = ray.GetPoint(Vector3.Distance(camera.transform.position, reference.position));
            return mousePoint;
        }

        public void UnselectEquipment()
        {
            onUnselectedObject?.Invoke(selectedObject);
            selectedObject = null;
        }

        public void SelectGameObject(GameObject selectedObject)
        {
            this.selectedObject = selectedObject;
            onSelectedObject?.Invoke(selectedObject);
        }
        #endregion

        #region Private Methods
        #endregion
    }
}