using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float minSpawnInterval = 0.8f;
    [SerializeField] float maxSpawnInterval = 2f;
    JumperAgent agent;

    public bool IsRunning { get; set; } = true;
    public int SpawnCount { get; private set; }

    void Start()
    {
        agent = transform.parent.Find("Agent").GetComponent<JumperAgent>();
        StartCoroutine(DoCheck());
    }

    public void ResetSpawner()
    {
        SpawnCount = 0;
    }

    IEnumerator DoCheck()
    {
        while(true)
        {
            if (IsRunning)
                SpawnObstacle();
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private void SpawnObstacle()
    {
        var obstacle = Instantiate(Resources.Load<Obstacle>("Obstacle"));
        //devilspawn.transform.localPosition = Vector3.zero;
        obstacle.transform.parent = transform;
        obstacle.transform.localPosition = Vector3.zero;
        SpawnCount++;
        agent.CheckEpisodeDuration(SpawnCount);
    }
}
