using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentScript : Agent
{
    private Rigidbody rb;
    private float jumpForce = 10f;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

        float jumpAction = actionBuffers.ContinuousActions[0];
        if (jumpAction > 0f)
        {
            Jump();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {

        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = 0f;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
       
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
