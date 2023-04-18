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

    void GiveReward()
    {
        var agent = transform.parent.parent.Find("Agent").GetComponent<JumperAgent>();
        agent.Reward();
    }

    void Update()
    {
        currentPosition += Time.deltaTime * speed;
        transform.localPosition = Vector3.Lerp(startLocation, endLocation, currentPosition);
        if (currentPosition > 1)
        {
            GiveReward();
            Destroy(gameObject);
        }
    }
}
