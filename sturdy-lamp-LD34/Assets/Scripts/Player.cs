using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        public string Name;
        public float Cost;
    };
    public List<SpawnEntry> Spawns = new List<SpawnEntry>();

    void Start() {


    }

    void Update() {

        Cash += BaseIncome * Time.deltaTime;
        
        if( (SpawnTimer -= Time.deltaTime) < 0 )
            foreach( var se in Spawns ) {
                if(Input.GetKeyDown(se.Key) && se.Cost < Cash ) {
                    Cash -= se.Cost;
                    Debug.Log("Player "+Id+"  spawn: "+se.Name);
                    SpawnTimer = SpawnDelay;
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
