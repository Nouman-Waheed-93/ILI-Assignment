﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireTruckStoreApp
{
    public class ObjectSpawner : MonoBehaviour
    {
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
                    newObject.GetComponent<Equipment>().Initialize(container);
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