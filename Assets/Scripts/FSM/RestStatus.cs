using UnityEngine;
using System.Collections;
public class RestStatus : IActualStatus
{
    private readonly FSMGrandParent fsm;
    private float timer;
    private float waitTime;
    private Animator animator; 

    public RestStatus (FSMGrandParent fSMGrandParent)
    {
        fsm = fSMGrandParent;
        timer=0f;
        waitTime=5f;
        animator = fsm.GetComponent<Animator>();

    }
    public void UpdateStatus()
    {
        animator.enabled=false;
        fsm.navMeshAgent.speed=0;

        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            timer = 0;
            ToWanderStatus();
        }
    }
    public void ToWanderStatus(){
        fsm.actualStatus = fsm.wanderStatus;

    }
    public void ToRestStatus(){

    }
    public void OnTriggerEnter(Collider other) {
        
    }
    public void OnTriggerExit(Collider other) {
    }
    
}