using UnityEngine;
using System.Collections;
public class WanderStatus : IActualStatus
{
    private readonly FSMGrandParent fsm;
    private Vector3 targetPosition;
    private bool isNearBanch;
    private Animator animator; 
    private bool hasExitedBanchArea;

    public WanderStatus (FSMGrandParent fSMGrandParent)
    {
        fsm = fSMGrandParent;
        isNearBanch=false;
        animator = fsm.GetComponent<Animator>();
        hasExitedBanchArea = true;
    }
    

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 20f;
        randomDirection += fsm.transform.position;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, 10f, UnityEngine.AI.NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;
        fsm.navMeshAgent.SetDestination(finalPosition);
    }

    public void UpdateStatus()
    {
        animator.enabled = true;
        fsm.navMeshAgent.speed=10;

        if (!fsm.navMeshAgent.pathPending && fsm.navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomDestination();

            if (isNearBanch && hasExitedBanchArea)
            {
                isNearBanch=false;
                fsm.actualStatus = fsm.restStatus;
            }
        }
    }

    public void ToWanderStatus(){

    }
    public void ToRestStatus(){
        fsm.actualStatus = fsm.restStatus;
    }
   
     public void OnTriggerEnter(Collider other) {
        
        if (other.CompareTag("banch"))
        {
            isNearBanch = true;
        }
    }
    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("banch"))
        {
            isNearBanch = false;
            hasExitedBanchArea = true; 
            
        }
    }
}