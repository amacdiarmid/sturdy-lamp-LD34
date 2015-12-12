using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum team
{
    Left = 9,
    Right = 8,
}

public class Player : MonoBehaviour {

    public int Id = 0;

    public float HP = 100;
    public float Cash = 100;

    public float BaseIncome = 1.0f;
    public float SpawnDelay = 0.2f;

    float SpawnTimer = -1;

    [System.Serializable]
    public class SpawnEntry {
        public KeyCode Key;
        public GameObject Fab;
        public float Cost;
    };
    public List<SpawnEntry> Spawns = new List<SpawnEntry>();

    [System.Serializable]
    public class LaneEntry {
        public KeyCode Key;
        public laneMarker Lane;
       // public string Name;
    };
    public List<LaneEntry> Lanes = new List<LaneEntry>();
    public int CurLane = 0;

    void Start() {


    }

    void Update() {

        Cash += BaseIncome * Time.deltaTime;

        for(int i = Lanes.Count; i-- > 0; ) {
            var ln = Lanes[i];
            if(Input.GetKeyDown(ln.Key)) {
                CurLane = i;
            }
        }

        if((SpawnTimer -= Time.deltaTime) < 0)
            foreach(var se in Spawns) {
                if(Input.GetKeyDown(se.Key) && se.Cost < Cash) {
                    Cash -= se.Cost;
                    Debug.Log("Player "+Id+"  spawn: ");
                    SpawnTimer = SpawnDelay;

                    var ln = Lanes[CurLane].Lane;
                    var go = Instantiate(se.Fab);
                    var c = go.GetComponent<CreepAI>();
                    c.lane = ln;
                    c.path = ln.paths[Id];
                    if (c.engineer != true)
                    {
                        if (Id == 0)
                        {
                            go.layer = (int)team.Left;
                            c.EnemyMask = 1 << (int)team.Right;
                            c.side = Id;
                        }
                        else
                        {
                            go.layer = (int)team.Right; 
                            c.EnemyMask = 1 << (int)team.Left;
                            c.side = Id;
                        }
                    }
                    else
                    {
                        if (Id == 0)
                        {
                            go.layer = (int)team.Left;
                            c.EnemyMask = 1 << (int)team.Left;
                            c.side = Id;
                        }
                        else
                        {
                            go.layer = (int)team.Right;
                            c.EnemyMask = 1 << (int)team.Right;
                            c.side = Id;
                        }
                    }
                    ln.creepList[Id].push(c);
                    
                   
                    break;
                }
            }

    }
    
    void OnGUI() {
        int x = 10, wid = 200;
        if( Id != 0 ) x = Screen.width - x -wid;
        GUI.Label( new Rect(x,10,wid,50),"Cash: "+Cash );
    }

}
