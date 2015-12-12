using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class laneMarker : MonoBehaviour {

    public List<pathMarker> path;
<<<<<<< HEAD
    public List<pathMarker> revPath;

=======
    public List<pathMarker> revPath;

>>>>>>> refs/remotes/origin/master
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
<<<<<<< HEAD
        revPath.Reverse();

        paths[0] = path;
=======
        revPath.Reverse();

        paths[0] = path;
>>>>>>> refs/remotes/origin/master
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
}
