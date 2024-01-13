using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public int melee;
    public GameObject meleePrefab;
    public GameObject monitor;
    void Start()
    {
       
        createRow(melee, -2f, meleePrefab);
    }

     void createRow(int num, float z, GameObject pf)
    {
        float pos = 1 - num;
        for (int i = 0; i < num; ++i) {
            Vector3 position = monitor.transform.TransformPoint(new Vector3 (pos,0f,z));
            GameObject temp = (GameObject)Instantiate(pf, position, monitor.transform.rotation); 
            temp.AddComponent<Formation>();
            temp.GetComponent<Formation>().pos = new Vector3 (pos,0,z);
            temp.GetComponent<Formation>().target = monitor;
            pos += 2f;
        }
    }
}
