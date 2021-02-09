using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FireTruckStoreApp
{
    public class ObjectSpawner : MonoBehaviour
    {
        public EquipmentSelectionEvent onEquipmentSpawned;
        public UnityEvent onFuelTankSpawned;
        public UnityEvent onFuelTankDeleted;
        public UnityEvent onBoltCutterSpawned;
        public UnityEvent onBoltCutterDeleted;

        [SerializeField]
        SpaceOccupier boltCutterPrefab;
        [SerializeField]
        SpaceOccupier fuelTankPrefab;
        [SerializeField]
        EquipmentContainer[] containers;
        [SerializeField]
        Transform spawnPoint;
        
        public void SpawnBoltCutter()
        {
            SpaceOccupier newBoltCutter = Instantiate<SpaceOccupier>(boltCutterPrefab);
            newBoltCutter.GetComponent<Equipment>().onDelete.AddListener(OnBoltCutterDeleted);
            if (SpawnObject(newBoltCutter))
                onBoltCutterSpawned?.Invoke();
        }

        public void SpawnFuelTank()
        {
            SpaceOccupier newFuelTank = Instantiate<SpaceOccupier>(fuelTankPrefab);
            newFuelTank.GetComponent<Equipment>().onDelete.AddListener(OnFuelTankDeleted);
            if (SpawnObject(newFuelTank))
                onFuelTankSpawned?.Invoke();
        }

        private void OnFuelTankDeleted(Equipment fuelTank)
        {
            onFuelTankDeleted?.Invoke();
        }

        private void OnBoltCutterDeleted(Equipment boltCutter)
        {
            onBoltCutterDeleted?.Invoke();
        }

        //returns true if spawned successfully
        private bool SpawnObject(SpaceOccupier newObject)
        {
            int containerIndex = 0;
            Vector3 spawnPosition = spawnPoint.position;
            while (containerIndex < containers.Length)
            {
                EquipmentContainer container = containers[containerIndex];
                if (container.Capacity.y >= newObject.Volume.y)
                {
                    spawnPosition.y = container.transform.position.y + newObject.Volume.y * 0.5f;
                    newObject.transform.position = spawnPosition;
                    Equipment equipment = newObject.GetComponent<Equipment>();
                    equipment.Initialize(container);
                    onEquipmentSpawned?.Invoke(equipment);
                    return true;
//                    break;
                }
                containerIndex++;
                if(containerIndex >= containers.Length)
                {
                    Destroy(newObject.gameObject);
                    return false;
                }
            }
            return true;
        }
    }
}