using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class RotationGizmo : MonoBehaviour
    {
        Equipment target;
        PositionFollower follower;

        private void Start()
        {
            DragHandler dragHandler = DragHandler.singleton;
            dragHandler.onSelectedObject.AddListener(SetTarget);
            dragHandler.onUnselectedObject.AddListener(TargetUnselected);
            dragHandler.onDragStarted.AddListener(Disactivate);
            dragHandler.onDragEnded.AddListener(Activate);
            Disactivate();
            follower = GetComponent<PositionFollower>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (RotationArrow.activeArrow)
                    RotationEnded();
            }
            if(target)
                transform.rotation = this.target.transform.rotation;
        }

        public void SetTarget(GameObject selectedGameObject)
        {
            Equipment selectedEquipment = selectedGameObject.GetComponent<Equipment>();
            if (selectedEquipment && !selectedEquipment.isPlaced)
            {
                this.target = selectedEquipment;
                follower.target = target.transform;
                Activate();
            }
        }

        public void TargetUnselected(GameObject unselectedGameObject)
        {
            Disactivate();
        }

        public void RotationStarted()
        {
            DragHandler.singleton.DisactivateDrag();
        }
        
        public void Rotate(float direction)
        {
            Vector3 rotationEulers = new Vector3(0, direction, 0);
            transform.Rotate(rotationEulers, Space.Self);
            Quaternion rotation = transform.rotation;

            if (target)
                target.Rotate(rotation);

            transform.Rotate(-rotationEulers, Space.Self);
        }

        public void RotationEnded()
        {
            RotationArrow.activeArrow = null;
            DragHandler.singleton.ActivateDrag();
        }

        private void Activate()
        {
            if (!target.isPlaced)
                gameObject.SetActive(true);
        }

        private void Disactivate()
        {
            gameObject.SetActive(false);
        }

    }
}
