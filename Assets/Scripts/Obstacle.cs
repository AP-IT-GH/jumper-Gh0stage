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

    void Start()
    {
        startLocation = transform.localPosition;
        endLocation = new Vector3(startLocation.x + 100, startLocation.y, startLocation.z);
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
