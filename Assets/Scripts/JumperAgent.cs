using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class JumperAgent : Agent
{
    [SerializeField] private float jumpForce = 2f;

    [SerializeField] private float rewardOnScore = 0.25f;
    [SerializeField] private float rewardOnFail = -0.2f;

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

        spawnerScript.ResetSpawner();

        for (int i = spawner.childCount-1; i >= 0 ; i--)
        {
            Destroy(spawner.GetChild(i).gameObject);
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        int jumpAction = actionBuffers.DiscreteActions[0];
        //Debug.Log(jumpAction);
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

        if (spawnerScript.SpawnCount >= 7)
        {
            EndEpisode();
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
        if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            AddReward(rewardOnFail);
        }
    }

    public void Reward()
    {
        AddReward(rewardOnScore);
        Debug.Log(GetCumulativeReward());
    }

    public override void CollectObservations(VectorSensor sensor) {    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
