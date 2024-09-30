using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStars : MonoBehaviour
{

    public GameObject[] objectToSpawn;
    private int index;
    public float spawnRadius = 5f;

    public float minDistanceBetweenObjects = 1f;
    public int maxAttempts = 100;

    private void Start()
    {
        for (int i = 0; i < 70; i++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool positionFound = false;

        for (int attempts = 0; attempts < maxAttempts; attempts++)
        {
            Vector3 potentialPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            potentialPosition.y = 0;

            if (IsPositionFree(potentialPosition))
            {
                spawnPosition = potentialPosition;
                positionFound = true;
                break;
            }
        }

        if (positionFound)
        {
            RussianRoulette();
            Instantiate(objectToSpawn[index], spawnPosition, Quaternion.identity);
        }
    }

    private bool IsPositionFree(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, minDistanceBetweenObjects);
        return colliders.Length == 0;
    }

    private void RussianRoulette()
    {
        int i = Random.Range(0, 20);
        if (i <= 15)
        {
            index = 0;
        }
        if (i >= 17)
        {
            index = 1;
        }
        else if(i > 15 && i < 17)
        {
            index = 2;
        }
    }

}
