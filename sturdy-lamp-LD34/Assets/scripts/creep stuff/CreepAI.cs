using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepAI : MonoBehaviour {

    //path staff
    public laneMarker lane;
    public List<pathMarker> path;
    float startTime;
    float journeyLength;
    int curMarkerPassed = 0;
    public float speed = 1;
    [Range(0, 1)] float attackCheck = 0.1f;
    float lastCheck;
    public bool move;
    [HideInInspector] public float distance;
    [HideInInspector] public CreepAI Next;
    [HideInInspector] public CreepAI Prev;
    //attack stuff
    public float damage;
    public float range;
    public float hp;

    // Use this for initialization
    void Start ()
    {
        startTime = Time.time;
        //Debug.Log(path[curMarkerPassed].postition + " " + path[curMarkerPassed + 1].postition);
        journeyLength = Vector3.Distance(path[curMarkerPassed].transform.position, path[curMarkerPassed + 1].transform.position);
        //Debug.Log("length" + journeyLength);
        move = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (move == true)
        {
            distance = Vector3.Distance(transform.position, path[curMarkerPassed + 1].transform.position);
            for (int i = curMarkerPassed + 1; i < path.Count; i++)
            {
                distance += Vector3.Distance(path[i - i].transform.position, path[i].transform.position);
            }

            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition, fracJourney);
            //Debug.Log(fracJourney);
            if (fracJourney >= 1)
            {   
                Debug.Log("next pos" + distCovered);
                curMarkerPassed++;
                startTime = Time.time;
                journeyLength = Vector3.Distance(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition);
    }
}
