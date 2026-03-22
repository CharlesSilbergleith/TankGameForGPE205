using UnityEngine;
using System.Collections.Generic;

public class SpawnSpawner : MonoBehaviour
{
    public List<GameObject> spawners;

    void Start()
    {
        GameObject tempSpawner = GetRandomSpawner();
        Instantiate(tempSpawner, transform.position, Quaternion.identity);
    }

    public GameObject GetRandomSpawner()
    {
      

        int randomIndex = Random.Range(0, spawners.Count);
        return spawners[randomIndex];
    }
}