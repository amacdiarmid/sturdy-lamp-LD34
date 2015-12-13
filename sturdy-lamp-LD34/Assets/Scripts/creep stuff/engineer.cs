using UnityEngine;
using System.Collections;

public class engineer : CreepAI {

    public float repairAmout;
    public float repairRange;
    public float repairRateOfFire;

    // Update is called once per frame
    void Update () {
		if (checkedRepair() == false)
		{
			checkAttack();
		}
        subUpdateCauseMyNamesArentGoodEnougthForjim();
    }

    bool checkedRepair()
    {
        //Debug.Log("update enginarrrr square");
        if ((FireTimer -= Time.deltaTime) < 0)
        {
            Health target = null;
            float mnD = float.MaxValue;
			Debug.Log(this.gameObject.layer);
            foreach (var col in Physics2D.OverlapCircleAll(Trnsfrm.position, repairRange, 1<<this.gameObject.layer))
            {
                Debug.Log(col.name);
                float d = (col.transform.position - Trnsfrm.position).sqrMagnitude;
                if (d < mnD)
                {
                    if (col.tag == "tower" && col.GetComponent<Tower>().maxHP != col.GetComponent<Tower>().hp)
                    {
                        //Debug.Log("tower close");
                        target = col.GetComponent<Health>();
                    }
                }
            }
            if (target != null)
            {
                //target->die mutha fucker
                //Debug.Log("creep " +this.name +" pew tower " +towerTarget.name);
                target.hp += repairAmout;
                FireTimer += repairRateOfFire;
                otherTimer = repairRateOfFire + 0.5f;
				return true;
            }
            else
            {
                FireTimer += 0.15f;
				return false;
            }
        }
		return false;
    }
}
