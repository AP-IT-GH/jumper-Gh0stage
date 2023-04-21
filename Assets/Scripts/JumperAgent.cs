using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class JumperAgent : Agent
{
    [SerializeField] private float jumpForce = 13.0f;

    [SerializeField] private float rewardOnScore = 1f;
    [SerializeField] private float rewardOnFail = -1.0f;
    [SerializeField] private float jumpPenalty = -0.5f;
    [SerializeField] public const int maxEpisodeDuration = 7;

    private SphereCollider hitbox;
    private Rigidbody rb;

    private bool isJumping = false;
    private bool isFalling = false;

    private Transform spawner;
    private SpawnerBehaviour spawnerScript;

    public override void Initialize()
    {
        hitbox = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        rb.mass = 2;
        spawner = gameObject.GetComponentInParent<Transform>().parent.Find("Spawner");
        spawnerScript = spawner.gameObject.GetComponent<SpawnerBehaviour>();
    }

    public override void OnEpisodeBegin()
    {
        Vector3 position = transform.localPosition;
        transform.localPosition = new Vector3(position.x, 1.5f, position.z);

        isJumping = false;
        isFalling = false;

        spawnerScript.ResetSpawner();

        for (int i = spawner.childCount-1; i >= 0 ; i--)
        {
            Destroy(spawner.GetChild(i).gameObject);
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        int jumpAction = actionBuffers.DiscreteActions[0];
        if (jumpAction > 0f && !isJumping)
        {
            isJumping = true;
            Jump();
        }

        if (isJumping && rb.velocity.y < 0)
        {
            isFalling = true;
        }

        if (isFalling && rb.velocity.y == 0)
        {
            isJumping = false;
            isFalling = false;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1:0;
        //Debug.Log(Input.GetKey(KeyCode.Space));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ping " + other.tag);

        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle collided: " + other.tag);

            Destroy(other.gameObject);
            AddReward(rewardOnFail);
            EndEpisode();
        }

    }

    public void CheckEpisodeDuration(int obstaclesSpawned)
    {
        if(obstaclesSpawned >= maxEpisodeDuration)
        {
            Debug.Log("Episode ended without casualties!");
            EndEpisode();
        }
    }
    public void Reward()
    {
        //Debug.Log("Reward before: "+GetCumulativeReward());
        AddReward(rewardOnScore);
        //Debug.Log("Reward after: "+GetCumulativeReward());
    }

    public override void CollectObservations(VectorSensor sensor) {    }

    private void Jump()
    {
        AddReward(jumpPenalty);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public static explicit operator JumperAgent(GameObject v)
    {
        throw new NotImplementedException();
    }
}
