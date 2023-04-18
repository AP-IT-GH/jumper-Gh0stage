using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class JumperAgent : Agent
{
    private SphereCollider hitbox;
    private Rigidbody rb;
    [SerializeField] private float jumpForce = 2f;
    private float basePosition;
    private bool isJumping = false;

    public override void Initialize()
    {
        hitbox = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        basePosition = transform.localPosition.y;
        Debug.Log(basePosition);
    }

    public override void OnEpisodeBegin()
    {
        Vector3 position = this.transform.localPosition;
        position = new Vector3(position.x, 1.5f, position.z);
        var spawner = gameObject.GetComponentInParent<Transform>().parent.Find("Spawner");
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

        if (isJumping && Mathf.Approximately(transform.localPosition.y, basePosition) && Mathf.Approximately(rb.velocity.y, 0))
        {
            isJumping = false;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1:0;
        //Debug.Log(Input.GetKey(KeyCode.Space));
    }

    public override void CollectObservations(VectorSensor sensor) {    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
