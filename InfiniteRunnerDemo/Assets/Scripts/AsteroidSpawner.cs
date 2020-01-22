using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Reference to the asteroid we will spawn
    public GameObject asteroidPrefab;
    // Spawn positions for the asteroids
    public Vector3[] asteroidSpawnPos;

    public float spawnInterval = 1;
    private float t = 0;

    // Update is called once per frame
    void Update()
    {
        // Adds seconds between frames to our spawn timer
        t += Time.deltaTime;
        if(t >= spawnInterval)
        {
            // if our spawn timer is pased the interval, we will spawn asteroids

            // Generates an index to remove an astorid from the spawner
            int clearIndex = Random.Range(0, asteroidSpawnPos.Length);
            List<GameObject> asteroidWave = new List<GameObject>();
            for (int x = 0; x < asteroidSpawnPos.Length; x++)
            {
                GameObject newAsteroid = Instantiate(asteroidPrefab, asteroidSpawnPos[x], Quaternion.identity);
                newAsteroid.name = "Asteroid";
                asteroidWave.Add(newAsteroid);
            }
            // Gets the asteroid that needs to be cleared and makes its sprite invisible
            asteroidWave[clearIndex].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            // Changes the name of the gameObject for our points collision code
            asteroidWave[clearIndex].name = "Points";

            // reset spawn timer
            t = 0;

            // Varries the spawn interval between 0.8 seconds and 2 seconds
            spawnInterval = Random.Range(0.8f, 2);
        }
        
    }
}
