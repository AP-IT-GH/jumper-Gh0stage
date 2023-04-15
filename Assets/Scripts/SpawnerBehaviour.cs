using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Obstacle> obstacles;
    [SerializeField] float minSpawnInterval = 0.8f;
    [SerializeField] float maxSpawnInterval = 2f;
    void Start()
    {
        StartCoroutine(DoCheck());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator DoCheck()
    {
        while(true)
        {
            spawnObstacle();
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private void spawnObstacle()
    {
        Instantiate(Resources.Load("Obstacle"));
    }
}
