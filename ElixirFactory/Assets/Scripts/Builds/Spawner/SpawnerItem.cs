using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItem : MonoBehaviour
{
    public GridModel gridModel;

    public Belt belt;
    public BeltItem currentItemOnBelt;
    public GameObject objectToSpawn;

    public Transform spawnPoint;

    public float spawnInterval = 2f;

    private float timeSinceLastSpawn = 0f;


    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObject();
            timeSinceLastSpawn = 0f;
        }

        if (belt.currentItemOnBelt != null && !belt.currentItemOnBelt.isMoving && belt.NextBelt() != null && belt.NextBelt().waitingItem.item == null)
        {
            belt.NextBelt().waitingItem = new(currentItemOnBelt, this.belt);
        }

        belt.CheckList();
    }

    void SpawnObject()
    {

        Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
    }
}
