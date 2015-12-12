using UnityEngine;
using System.Collections;

public class engineer : CreepAI {
	
	// Update is called once per frame
	void Update () {
        Debug.Log("update enginarrrr square");
        if ((FireTimer -= Time.deltaTime) < 0)
        {
            CreepAI creepTarget = null;
            Tower towerTarget = null;
            float mnD = float.MaxValue;

            foreach (var col in Physics2D.OverlapCircleAll(Trnsfrm.position, range, EnemyMask))
            {
                //Debug.Log(col.name);
                float d = (col.transform.position - Trnsfrm.position).sqrMagnitude;
                if (d < mnD)
                {
                    if (col.tag == "tower" && col.GetComponent<Tower>().maxHP != col.GetComponent<Tower>().hp)
                    {
                        Debug.Log("tower close");
                        towerTarget = col.GetComponent<Tower>();
                        creepTarget = null;
                    }
                    creepTarget = col.GetComponent<CreepAI>();
                }
            }
            if (towerTarget != null)
            {
                //target->die mutha fucker
                //Debug.Log("creep " +this.name +" pew tower " +towerTarget.name);
                towerTarget.hp += damage;
                FireTimer += rateOfFire;
                otherTimer = rateOfFire + 0.5f;
            }
            else
            {
                FireTimer += 0.15f;
            }
        }
        subUpdateCauseMyNamesArentGoodEnougthForjim();
    }

}
