using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BansheeGz.BGSpline.Curve;

public class AgentScript : MonoBehaviour
{
    public BGCurve curve;
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private float minSpeed = 5f; 
    private float maxSpeed = 15f; 
    private List<Vector3> pointsOnCurve; 
    private bool direction;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); 
        
        if (curve == null) return;
        pointsOnCurve = new List<Vector3>();
        for (int i = 0; i < curve.PointsCount; i++)
        {
            BGCurvePointI point = curve[i];
            pointsOnCurve.Add(point.PositionWorld);
        }
        currentWaypointIndex = Random.Range(0, pointsOnCurve.Count-1); 

        SetRandomSpeed();
        SetRandomDirection();
        SetDestination();
        
    }
     
    void SetRandomSpeed(){
        agent.speed = Random.Range(minSpeed, maxSpeed); 

    }

    void SetDestination()
    {
        if (pointsOnCurve.Count == 0) return; 
        agent.SetDestination(pointsOnCurve[currentWaypointIndex]); 
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (direction)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % pointsOnCurve.Count;
            }
            else
            {
                currentWaypointIndex = (currentWaypointIndex - 1 + pointsOnCurve.Count) % pointsOnCurve.Count;
            }

            SetDestination();
        }
    }

     void SetRandomDirection()
    {
          int num = Random.Range(0, 2); 
          if(num==0){
            direction=true;
          }else{
            direction=false;
          }
    }
}
