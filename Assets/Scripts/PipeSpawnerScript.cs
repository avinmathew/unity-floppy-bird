using System;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipePrefab;
    public float startSpawnRate = 4;
    public int increaseSpawnScore = 5; // Increase spawn every x points
    public float heightOffset = 2.5f;
    
    private float timer = 0;
    private float spawnRate;

    void Start()
    {
        spawnRate = startSpawnRate;
        timer = spawnRate; // So we spawn at the beginning without having to wait
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.IsGameStart)
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                float lowestPoint = transform.position.y - heightOffset;
                float highestPoint = transform.position.y + heightOffset;
                float randomHeight = UnityEngine.Random.Range(lowestPoint, highestPoint);
                Instantiate(pipePrefab, new Vector3(transform.position.x, randomHeight, 0), transform.rotation);
                timer = 0;
            }

            // Increase speed every "increaseSpawnScore" points. Divide by 2 to lengthen the game. Don't go lower than 1.5 as it's impossible
            spawnRate = Math.Max(startSpawnRate - GameState.Score / increaseSpawnScore / 2, 1.5f);
        }
    }
}
