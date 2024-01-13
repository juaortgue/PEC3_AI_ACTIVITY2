using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManagement : MonoBehaviour
{
    public bool canFollow;
    
    public void FollowThief(){
        Debug.Log($"FOLLOW THIEF");
        canFollow=true;
    } 
    public void UnFollowThief(){
        Debug.Log($"UNFOLLOW");
        canFollow=false;
    } 
   
}
