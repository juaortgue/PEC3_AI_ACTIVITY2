using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefManagement : MonoBehaviour
{
    public bool stolen;
    
    public void RestartThief(){
        Debug.Log($"RESTART THIEF");
        stolen=false;
    } 
}
