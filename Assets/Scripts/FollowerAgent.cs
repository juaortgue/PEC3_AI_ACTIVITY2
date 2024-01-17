using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors; 
using Unity.MLAgents.Actuators;
public class FollowerAgent : Agent
{
    Rigidbody rBody;
    public float forceMultiplier = 10;
    public Transform Target;
    public string targetName;
    public  float minX,maxX,minZ,maxZ,targetY,followerY;
    void Start () {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
       // If the Agent fell, zero its momentum
        Vector3 actualPosition = this.transform.position;
        if (actualPosition.y < followerY || actualPosition.x<minX || actualPosition.x>maxX || actualPosition.z<minZ || actualPosition.z>maxZ)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3( 41.44f, followerY, -5.03f);
        }

        // Move the target to a new spot
        Target.localPosition = new Vector3(Random.Range(minX,maxX),
                                           targetY,
                                           Random.Range(minZ,maxZ));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        Vector3 actualPosition = this.transform.position;
        if (actualPosition.y < followerY || actualPosition.x<minX || actualPosition.x>maxX || actualPosition.z<minZ || actualPosition.z>maxZ)
        {
            EndEpisode();
        }
    }
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            var continuousActionsOut = actionsOut.ContinuousActions;
            continuousActionsOut[0] = Input.GetAxis("Horizontal");
            continuousActionsOut[1] = Input.GetAxis("Vertical");
        }
        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.name==targetName)
            {
                 DoSuccessAction();
            }
        }
        private void DoSuccessAction(){
            SetReward(1.0f);
            EndEpisode();
        }
}
