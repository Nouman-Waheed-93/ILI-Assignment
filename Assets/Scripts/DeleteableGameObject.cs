using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class DeleteableGameObject : MonoBehaviour
    {
        public GameObjectEvent onDelete;

        public void Delete()
        {
            onDelete.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}