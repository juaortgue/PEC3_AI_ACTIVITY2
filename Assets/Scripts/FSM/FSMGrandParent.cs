using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMGrandParent : MonoBehaviour
{
    [HideInInspector] public IActualStatus actualStatus;
    [HideInInspector] public WanderStatus wanderStatus;
    [HideInInspector] public RestStatus restStatus;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    void Start()
    {
     actualStatus = wanderStatus;   
    }
    private void Awake()
    {
        wanderStatus = new WanderStatus (this);
        restStatus = new RestStatus (this);
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
    }
    void Update()
    {
        actualStatus.UpdateStatus();
    }
    private void OnTriggerEnter(Collider other)
    {
        actualStatus.OnTriggerEnter (other);
    }
    void OnTriggerExit(Collider other)
    {
        actualStatus.OnTriggerExit(other);
    }
}
