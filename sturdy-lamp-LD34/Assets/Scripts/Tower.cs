using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    public float Dmg = 1;
    public float Range = 10;
    public float RoF = 1;

    public LayerMask EnemyMask;

    float FireTimer = 0.5f;

    Transform Trnsfrm;

    void Start() {
        Trnsfrm = transform;

    }

    void Update() {
        if((FireTimer -= Time.deltaTime) < 0) {
            CreepAI target = null; float mnD = float.MaxValue;
            foreach(var col in Physics2D.OverlapCircleAll(Trnsfrm.position, Range, EnemyMask.value)) {

                float d = (col.transform.position - Trnsfrm.position).sqrMagnitude;
                if(d < mnD) {
                    d = mnD;
                    target = col.GetComponent<CreepAI>();
                }
            }
            if(target != null ) {
                //target->die mutha fucker
                Debug.Log("pew");
                target.hp -= Dmg;
                FireTimer += RoF;
            } else {
                FireTimer += 0.15f;
            }
        }
    }


}
