using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    private float speed;
    private Vector3 cohesion,align,separation,direction;

    void Move()
    {
        LimitMovement();
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                            Quaternion.LookRotation(direction),
                                            myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);

    }
    void LimitMovement(){
        float error = 0.5f;
        float step = Time.deltaTime * 5f;
        if(!(transform.position.z>=myManager.minZ+error && transform.position.z<=myManager.maxZ-error)){
            Vector3 targetPosition = new Vector3(transform.position.x,transform.position.y,Random.Range(myManager.minZ, myManager.maxZ));
            transform.position = targetPosition;
        }
        if (!(transform.position.y>=myManager.minY+error && transform.position.y<=myManager.maxY-error))
        {

            Vector3 targetPosition = new Vector3(transform.position.x,Random.Range(myManager.minY, myManager.maxY),transform.position.y);

            transform.position = targetPosition;

        }
        if (!(transform.position.x>=myManager.minX+error && transform.position.x<=myManager.maxX-error))
        {
            
            Vector3 targetPosition = new Vector3(Random.Range(myManager.minX, myManager.maxX),transform.position.y,transform.position.z);

            transform.position =targetPosition;

        }
    }


    void Update()
    {
        ApplyFlock();
        Move();
    }
    void ApplyFlock(){
        CalculateCohesion();
        CalculateAlign();
        CalculateSeparation();
        direction = (cohesion + align + separation).normalized * speed;
    }
    void CalculateSeparation(){
        separation = Vector3.zero;
        foreach (GameObject go in myManager.allInsects) {
            if (go != this.gameObject) {
                float distance = Vector3.Distance(go.transform.position, 
                                                transform.position);
                if (distance <= myManager.neighbourDistance){
                    Vector3 separationForce = (transform.position - go.transform.position).normalized;
                    separation += separationForce / distance;
                }
                    
            }
        }
    }
    void CalculateAlign(){
        align = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allInsects) {
            if (go != this.gameObject) {
                float distance = Vector3.Distance(go.transform.position, 
                                                transform.position);
                if (distance <= myManager.neighbourDistance) {
                    align += go.GetComponent<Flock>().transform.forward;
                    num++;
                }
            }
        }
        if (num > 0) {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }else {
        // Velocidad predeterminada cuando no hay vecinos
        speed = myManager.defaultSpeed;
        }
    }
    void CalculateCohesion(){
        int num = 0;
        cohesion = Vector3.zero;
        foreach (GameObject go in myManager.allInsects) {
            if (go != this.gameObject) {
                float distance = Vector3.Distance(go.transform.position, 
                                                transform.position);
                if (distance <= myManager.neighbourDistance) {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }
        if (num > 0){
            cohesion = (cohesion / num - transform.position).normalized * speed;
        }
    }


}
