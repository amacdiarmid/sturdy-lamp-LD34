using UnityEngine;
using System.Collections;

public class Tower : Health
{
    float FireTimer = 0.5f;
    Transform Trnsfrm;

    void Start()
    {
        Trnsfrm = transform;
    }

    void Update()
    {
        if ((FireTimer -= Time.deltaTime) < 0)
        {
            Health target = null; float mnD = float.MaxValue;
            foreach (var col in Physics2D.OverlapCircleAll(Trnsfrm.position, range, EnemyMask.value))
            {

                float d = (col.transform.position - Trnsfrm.position).sqrMagnitude;
                if (d < mnD && col.tag != "tower")
                {
                    d = mnD;
                    target = col.GetComponent<Health>();
                }
            }
            if (target != null)
            {
                //target->die mutha fucker
                Debug.Log("tower " + this.name +" pew creep " +target.name);
                target.hp -= damage;
                FireTimer += rateOfFire;
            }
            else
            {
                FireTimer += 0.15f;
            }
        }
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }


}
