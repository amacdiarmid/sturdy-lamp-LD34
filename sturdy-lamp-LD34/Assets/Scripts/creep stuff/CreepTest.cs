using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepTest : MonoBehaviour {

    public GameObject Creep;
    public GameObject CreepRange;
    GameObject node;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("mouse down");
        node = GameObject.Find("laneNode");
        GameObject temp = Instantiate(Creep);
        temp.layer = 9;
        temp.GetComponent<CreepAI>().lane = node.GetComponent<laneMarker>();
        temp.GetComponent<CreepAI>().path = temp.GetComponent<CreepAI>().lane.path;
        temp.GetComponent<CreepAI>().EnemyMask = 1<<8;
        temp.GetComponent<CreepAI>().side = 0;
        node.GetComponent<laneMarker>().creepList[0].push(temp.GetComponent<CreepAI>());

        temp = Instantiate(CreepRange);
        temp.layer = 8;
        temp.GetComponent<CreepAI>().lane = node.GetComponent<laneMarker>();
        temp.GetComponent<CreepAI>().path = temp.GetComponent<CreepAI>().lane.revPath;
        temp.GetComponent<CreepAI>().EnemyMask = 1 << 8;
        temp.GetComponent<CreepAI>().side = 0;
        node.GetComponent<laneMarker>().creepList[1].push(temp.GetComponent<CreepAI>());
    }
}
