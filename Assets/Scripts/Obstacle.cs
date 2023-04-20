using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Vector3 startLocation;
    Rigidbody rb;
    public float Speed { get; set; }
    [SerializeField] float distance;
    Vector3 velocity;

    void Start()
    {
        startLocation = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        velocity = new Vector3(Speed, 0, 0);
        rb.velocity = velocity;
    }

    void GiveReward()
    {
        var agent = transform.parent.parent.Find("Agent").GetComponent<JumperAgent>();
        agent.Reward();
    }

    void Update()
    {
        rb.velocity = velocity;
        if (transform.localPosition.x - startLocation.x > distance)
        {
            Debug.Log("passed distance");
            GiveReward();
            Destroy(gameObject);
        }
    }
}
