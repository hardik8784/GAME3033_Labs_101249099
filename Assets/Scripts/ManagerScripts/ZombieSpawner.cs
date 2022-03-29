using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int numberOfZombieToSpawn;
    public GameObject[] zombiePrefab;
    public SpawnerVolume[] spawnerVolumes;

    GameObject followGameObject;

    // Start is called before the first frame update
    void Start()
    {
        //followGameObject = GameObject.FindGameObjectWithTag("Player");

        for(int i=0;i<numberOfZombieToSpawn;i++)
        {
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        GameObject zombieToSpawn = zombiePrefab[Random.Range(0, zombiePrefab.Length)];
        SpawnerVolume spawnerVolume = spawnerVolumes[Random.Range(0, spawnerVolumes.Length)];

        //if (!followGameObject) return;

        GameObject zombie = Instantiate(zombieToSpawn, spawnerVolume.GetPositionInBounds(), spawnerVolume.transform.rotation);

        //zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
