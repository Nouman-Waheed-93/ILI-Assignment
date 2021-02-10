using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FireTruckStoreApp
{
    [System.Serializable]
    public class EquipmentEvent : UnityEvent<Equipment>
    {

    }

    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject>
    {

    }
}