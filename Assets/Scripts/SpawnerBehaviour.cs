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

    public bool IsRunning { get; set; } = true;
    public uint SpawnCount { get; private set; }

    void Start()
    {
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
        var devilspawn = Instantiate(Resources.Load<Obstacle>("Obstacle"));
        //devilspawn.transform.localPosition = Vector3.zero;
        devilspawn.transform.parent = transform;
        devilspawn.transform.localPosition = Vector3.zero;
        SpawnCount++;
    }
}
