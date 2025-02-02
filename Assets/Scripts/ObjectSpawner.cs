﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FireTruckStoreApp
{
    public class ObjectSpawner : MonoBehaviour
    {
        public static ObjectSpawner singleton;
        public GameObjectEvent onEquipmentSpawned;
        public UnityEvent onFuelTankSpawned;
        public UnityEvent onFuelTankDeleted;
        public UnityEvent onBoltCutterSpawned;
        public UnityEvent onBoltCutterDeleted;
        public UnityEvent onSpawnFailed;

        [SerializeField]
        SpaceOccupier boltCutterPrefab;
        [SerializeField]
        SpaceOccupier fuelTankPrefab;
        [SerializeField]
        SpaceOccupier ironBoxPrefab;
        [SerializeField]
        EquipmentContainer[] containers;
        [SerializeField]
        Transform spawnPoint;

        private void Awake()
        {
            singleton = this;
        }

        public void SpawnBoltCutter()
        {
            SpaceOccupier newBoltCutter = Instantiate<SpaceOccupier>(boltCutterPrefab);
            newBoltCutter.GetComponent<DeleteableGameObject>().onDelete.AddListener(OnBoltCutterDeleted);
            if (SpawnObject(newBoltCutter))
                onBoltCutterSpawned?.Invoke();
            else
                onSpawnFailed?.Invoke();
        }

        public void SpawnFuelTank()
        {
            SpaceOccupier newFuelTank = Instantiate<SpaceOccupier>(fuelTankPrefab);
            newFuelTank.GetComponent<DeleteableGameObject>().onDelete.AddListener(OnFuelTankDeleted);
            if (SpawnObject(newFuelTank))
                onFuelTankSpawned?.Invoke();
            else
                onSpawnFailed?.Invoke();
        }

        public void PlaceInIronBox(Equipment equipment)
        {
            SpaceOccupier ironBox = Instantiate(ironBoxPrefab);
            equipment.GetComponent<Collider>().enabled = false;
            equipment.GetComponent<BoundingBoxRenderer>().Hide();
            ironBox.transform.position = equipment.transform.position;
            equipment.transform.parent = ironBox.transform;
            ironBox.transform.rotation = equipment.transform.rotation;
            ironBox.transform.position = Utility.KeepPositionInsideContainer(ironBox, equipment.transform.position, equipment.currentContainer);
            equipment.currentContainer.OccupySpace(ironBox);
            ironBox.GetComponent<DeleteableGameObject>().onDelete.AddListener(OnIronBoxDeleted);
        }

        private void OnIronBoxDeleted(GameObject ironBox)
        {
            //a temporary solution, because there is only one object that can be placed in ironbox
            onBoltCutterDeleted?.Invoke();
        }

        private void OnFuelTankDeleted(GameObject fuelTank)
        {
            onFuelTankDeleted?.Invoke();
        }

        private void OnBoltCutterDeleted(GameObject boltCutter)
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
                    onEquipmentSpawned?.Invoke(equipment.gameObject);
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