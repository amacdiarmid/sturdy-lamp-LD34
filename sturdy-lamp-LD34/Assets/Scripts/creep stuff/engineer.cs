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
        if ((otherTimer -= Time.deltaTime) < 0)
        {
            if (Next != null)
            {
                if (curMarkerPassed > Next.getCurMarker() || (curMarkerPassed == Next.getCurMarker() && JourneyDelta > Next.getJournyDelta()))
                {
                    lane.creepList[side].swap(this, Next);
                }
            }
            //do killing stuff7
            distance = Vector3.Distance(transform.position, path[curMarkerPassed + 1].transform.position);
            for (int i = curMarkerPassed + 1; i < path.Count; i++)
            {
                distance += Vector3.Distance(path[i - i].transform.position, path[i].transform.position);
            }

            //float distCovered = (Time.time - startTime) * speed;
            //float fracJourney = distCovered / journeyLength;
            //transform.position = Vector3.Lerp(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition, fracJourney);
            //Debug.Log(fracJourney);
            JourneyDelta += Time.deltaTime * speed / journeyLength;
            if (JourneyDelta >= 1)
            {
                Debug.Log("next pos" + journeyLength);
                curMarkerPassed++;
                //startTime = Time.time;
                JourneyDelta -= 1.0f;
                journeyLength = Vector3.Distance(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition);
            }
            // transform.position = Vector3.Lerp(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition, JourneyDelta);
            transform.position = getPoint(JourneyDelta, curMarkerPassed);
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

}
