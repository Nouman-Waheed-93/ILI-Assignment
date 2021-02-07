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
            this.target = equipment;
            follower.target = target.transform;
            Activate();
        }

        public void RotationStarted()
        {
            DragHandler.singleton.DisactivateDrag();
        }
        
        public void Rotate(float direction)
        {
            Vector3 eulers = transform.rotation.eulerAngles;
            eulers.y += direction;
            Quaternion rotation = Quaternion.Euler(eulers);

            if (target)
                target.Rotate(rotation);
        }

        public void RotationEnded()
        {
            RotationArrow.activeArrow = null;
            DragHandler.singleton.ActivateDrag();
        }

        private void Activate()
        {
            gameObject.SetActive(true);
        }

        private void Disactivate()
        {
            gameObject.SetActive(false);
        }

    }
}
