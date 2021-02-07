using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FireTruckStoreApp
{
    public class ObjectSpawner : MonoBehaviour
    {
        public EquipmentSelectionEvent onEquipmentSpawned;

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
            SpawnObject(Instantiate<SpaceOccupier>(boltCutterPrefab));
        }

        public void SpawnFuelTank()
        {
            SpawnObject(Instantiate<SpaceOccupier>(fuelTankPrefab));
        }

        private void SpawnObject(SpaceOccupier newObject)
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
                    break;
                }
                containerIndex++;
                if(containerIndex >= containers.Length)
                {
                    Destroy(newObject.gameObject);
                }
            }
        }
    }
}