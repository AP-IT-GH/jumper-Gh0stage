using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Vector3 startLocation;
    Vector3 endLocation;
    float currentPosition;
    [SerializeField] float speed;

    [SerializeField] Agent agent;
    void Start()
    {
        currentPosition = 0;
        startLocation = transform.localPosition;
        endLocation = agent.transform.localPosition - new Vector3(startLocation.x + 10, 0, startLocation.z + 10);
    }

    void Update()
    {
        currentPosition += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startLocation, endLocation, currentPosition);
        if (currentPosition > 1)
        {
            Destroy(gameObject);
        }
    }
}
