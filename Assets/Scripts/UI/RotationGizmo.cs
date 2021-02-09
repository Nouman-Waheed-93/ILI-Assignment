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
            dragHandler.onSelectedEquipment.AddListener(SetTarget);
            dragHandler.onUnselectedEquipment.AddListener(TargetUnselected);
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

        public void SetTarget(Equipment equipment)
        {
            if (!equipment.isPlaced)
            {
                this.target = equipment;
                follower.target = target.transform;
                Activate();
            }
        }

        public void TargetUnselected(Equipment equipment)
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
