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
        temp.GetComponent<CreepAI>().lane = node.GetComponent<laneMarker>();
        temp.GetComponent<CreepAI>().path = temp.GetComponent<CreepAI>().lane.path;
        node.GetComponent<laneMarker>().creepList[0].push(temp.GetComponent<CreepAI>());

        temp = Instantiate(CreepRange);
        temp.GetComponent<CreepAI>().lane = node.GetComponent<laneMarker>();
        temp.GetComponent<CreepAI>().path = temp.GetComponent<CreepAI>().lane.revPath;
        node.GetComponent<laneMarker>().creepList[1].push(temp.GetComponent<CreepAI>());
    }
}
