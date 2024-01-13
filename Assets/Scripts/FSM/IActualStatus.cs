
using UnityEngine;
using System.Collections;
public interface IActualStatus
{
    void UpdateStatus();
    void ToWanderStatus();
    void ToRestStatus();
    void OnTriggerEnter(Collider other);
    void OnTriggerExit(Collider other);

}