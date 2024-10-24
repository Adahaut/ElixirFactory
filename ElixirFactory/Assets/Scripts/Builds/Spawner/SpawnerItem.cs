using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerItem : BuildProperties
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    public float spawnInterval = 2f;
    public GridModel gridModel;
    public BeltDirection direction;

    private float timeSinceLastSpawn = 0f;

    private bool canSpawn = true;

    private void Awake()
    {
        gridModel = GridModel.instance;
    }

    void Update()
    {
        
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObject();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnObject()
    {
        // Instancier l'objet
        if (canSpawn)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

            Belt closestBelt = FindBelt();
            if (closestBelt != null && closestBelt.currentItemOnBelt == null)
            {
                // Placer l'item sur la belt
                BeltItem beltItem = spawnedObject.GetComponent<BeltItem>();
                if (beltItem != null)
                {
                    closestBelt.currentItemOnBelt = beltItem;
                    beltItem.SetDestination(closestBelt.transform.position);  // Envoyer l'item vers la belt
                }
            }
            else
            {
                Debug.LogWarning("Pas de belt disponible");
                canSpawn = false;
            }
        }
    }

    // Fonction pour trouver la belt la plus proche
    Belt FindBelt()
    {
        int x = 0;
        int y = 0;

        if (direction == BeltDirection.LEFT) x = -1;
        if (direction == BeltDirection.RIGHT) x = 1;

        if (direction == BeltDirection.TOP) y = 1;
        if (direction == BeltDirection.DOWN) y = -1;

        //Rajouter condition > grid size
        if ((int)transform.position.x + x < 0 || (int)transform.position.y + y < 0)
            return null;

        if (gridModel.grid[(int)transform.position.x + x, (int)transform.position.y + y].TryGetComponent<Case>(out Case c) && c.GetObject() is Belt)
        {
            return c.GetObject() as Belt;
        }

        return null;
    }
}
