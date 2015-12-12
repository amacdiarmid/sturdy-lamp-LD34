using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class laneMarker : MonoBehaviour {

    public List<pathMarker> path;
    [HideInInspector] public List<pathMarker> revPath;
    public List<pathMarker>[] paths = new List<pathMarker>[2];

    public float totalDis;

    //0 for left to right, 1 for right to left
    public List<CreepList> creepList;

    // Use this for initialization
    void Start ()
    {
        for (int i = 1; i < path.Count; i++)
        {
            totalDis += Vector3.Distance(path[i - i].transform.position, path[i].transform.position);
        }
        creepList.Add(new CreepList());
        creepList.Add(new CreepList());
        revPath = new List<pathMarker>(path);
        revPath.Reverse();
        paths[0] = path;
        paths[1] = revPath;
    }
	
	// Update is called once per frame
	void Update () {
        if (creepList[0].First != null && creepList[1].First != null)
        {
            //if ((creepList[0].First.transform.position - creepList[1].First.transform.position).sqrMagnitude < creepList[0].First.range)
            //{
            //    creepList[0].First.move = false;
            //}
            //if ((creepList[0].First.transform.position - creepList[1].First.transform.position).sqrMagnitude < creepList[1].First.range)
            //{
            //    creepList[1].First.move = false;
            //}
        }
    }

    void OnDrawGizmos() {

        int iters = 15; Vector2 lp = path[path.Count-1].transform.position;
        path[path.Count-1].postition = path[path.Count - 1].transform.position;
        for (int i = path.Count-1; i-- > 0; ) {
            path[i].postition = path[i].transform.position;
            for(int j = iters; j-- > 0; ) {
                Vector2 p = CreepAI.getPoint((float)j / (float)iters, i, path);
                Gizmos.DrawLine(lp, p);
                lp =p;
            }
        }
    }
}
