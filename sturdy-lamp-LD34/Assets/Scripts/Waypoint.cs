using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    public LayerMask Mask;
    public float Radius = 5;

    float Timer;
    void Update() {
        if((Timer-=Time.deltaTime) < 0) {

            CreepAI[] hold = new CreepAI[CreepAI.SubLaneCnt];

            foreach( var col in Physics2D.OverlapCircleAll( transform.position, Radius, Mask.value) ) {
                var c = col.GetComponent<CreepAI>();

                if(c== null) continue;

                if(hold[c.SubLane] == null  || hold[c.SubLane].journeyDis > c.journeyDis )
                    hold[c.SubLane] = c;
               // c.otherTimer = Mathf.Max(c.otherTimer, 0.25f);
            }
            for(int i = CreepAI.SubLaneCnt; i-- >0; )
                if(hold[i] != null )
                    if((hold[i].transform.position - transform.position).sqrMagnitude < Radius * Radius) 
                         hold[i].otherTimer = Mathf.Max(hold[i].otherTimer, 0.25f);
            Timer = 0.1f;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, Radius);
    }

}
