﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepAI : Health
{

    public static int SubLaneCnt =5;
    public int SubLane  = 0;

	//path staff
	[HideInInspector]
	public laneMarker lane;
	[HideInInspector]
	public List<pathMarker> path;
	//float startTime;
	protected float JourneyDelta;
	[HideInInspector]
	public float journeyDis;
	protected float journeyLength;
	protected int curMarkerPassed = 0;
	public float speed = 1;
	protected float lastCheck;
	[HideInInspector]
	public bool move;
	[HideInInspector]
	public float distance;
	[HideInInspector]
	public CreepAI Next;
	[HideInInspector]
	public CreepAI Prev;
	//attack stuff
	protected float FireTimer = 0.5f;
	public  float otherTimer = -1f;
	protected Transform Trnsfrm;
	[HideInInspector]
	public int side;

    void sortLayer() {
        int so = SubLane;
        if(side != 0) SubLane = SubLaneCnt-SubLane-1;
        GetComponentInChildren<SpriteRenderer>().sortingOrder = so;
    }
    // Use this for initialization
    void Start()
    {
        journeyDis = 0;
        SubLane = Random.Range(0, SubLaneCnt);
        BounceMod = Random.Range(0.9f, 1.1f);
        sortLayer();
        Trnsfrm = transform;
        //startTime = Time.time;
        JourneyDelta = 0;
        //Debug.Log(path[curMarkerPassed].postition + " " + path[curMarkerPassed + 1].postition);
        journeyLength = Vector3.Distance(path[curMarkerPassed].transform.position, path[curMarkerPassed + 1].transform.position);
        //Debug.Log("length" + journeyLength);
        move = true;

		DesPos = path[0].postition;
		elUpdateDeMove();
		Trnsfrm.position = DesPos;
	}
	struct CubicSpline
	{
		public CubicSpline(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
		{
			M0 = 0.5f * d - 1.5f * c - 0.5f * a + 1.5f * b;
			M1 = a - 2.5f * b + 2.0f * c - 0.5f * d;
			M2 = 0.5f * c - 0.5f * a;
			M3 = b;
		}
		public Vector2 get(float delta)
		{
			float delta2 = delta * delta;
			return M0 * delta * delta2 + M1 * delta2 + M2 * delta + M3;
		}
		Vector2 M0, M1, M2, M3;
	};


	static public Vector2 getPoint(float t, int i2, List<pathMarker> path)
	{
		int i1 = i2 - 1, i3 = i2 + 1, i4 = i2 + 2;
		Vector2 p2 = path[i2].postition, p3 = path[i3].postition, p1, p4;
		if (i1 < 0) p1 = p2 + p2 - p3;
		else p1 = path[i1].postition;
		if (i4 >= path.Count) p4 = p3 + p3 - p2;
		else p4 = path[i4].postition;

		//t *= 0.25f; t+= 0.25f;
		CubicSpline s = new CubicSpline(p1, p2, p3, p4);
		return s.get(t);
	}

	protected Vector2 getPoint(float t, int i2)
	{
		return getPoint(t, i2, path);
	}

	Vector2 DesPos;

	[HideInInspector]
	public List<float> Cd;
	[HideInInspector]
	public float MxD;

	[HideInInspector]
	float laneTimer = 0;
	void elUpdateDeMove()
	{
		if ((otherTimer -= Time.deltaTime) < 0)
		{
			//   if(Next != null) {
			//if(curMarkerPassed > Next.curMarkerPassed || (curMarkerPassed == Next.curMarkerPassed && JourneyDelta > Next.JourneyDelta)) {

			if (Bounce >= 1.0f) Bounce -= 1.0f;

			float[] cd = new float[SubLaneCnt];
			for (int i = SubLaneCnt; i-- > 0;) cd[i] = float.MaxValue;
			//var n = Next;

			float colRad = 1.0f;
			//for(int i = SubLaneCnt*2; i-- >0; ) {
			foreach (var col in Physics2D.OverlapCircleAll(Trnsfrm.position, colRad * 1.2f, 1 << gameObject.layer))
			{
				if (col.gameObject == gameObject) continue;
				CreepAI n = col.GetComponent<CreepAI>();
				if (n == null) continue;
				if (n.journeyDis < journeyDis) continue;
				// if(n.journeyDis < journeyDis-0.25f ) {
				//     lane.creepList[side].swap(this, n);

				// } else {

				//float d = (Trnsfrm.position - n.Trnsfrm.position).sqrMagnitude;
				float d = n.journeyDis - journeyDis;

				if (cd[n.SubLane] > d)
				{
					cd[n.SubLane] = d;
				}

				//  if(d > 2.25f) break;
				//  }
				//n = n.Next;
				//  if(n == null) break;
			}

			float mxD = float.MinValue; int ci = SubLane;
			if ((laneTimer -= Time.deltaTime) < 0)
			{
				Cd = new List<float>();
				//for(int i = SubLaneCnt; i-- >0; ) {

				for (int i = 0; i < SubLaneCnt; i++)
				{
					float d = cd[i];
					//                    Cd.Add(d);
					if (d == float.MaxValue)
					{
						d = 5.0f - Mathf.Abs(i - SubLane);
					}
					Cd.Add(d);
					if (d > mxD)
					{
						mxD = d;
						ci = i;
					}
				}
				MxD = mxD;
				if (mxD < colRad)
				{
					laneTimer = 0.1f;
					return;
				}
				if (SubLane != ci)
				{
					SubLane = ci;
					laneTimer = 0.8f;
					sortLayer();
				}
				else laneTimer = 0.1f;
			}
			else
			{
				if (cd[SubLane] < colRad) return;
				laneTimer = 0.1f;
			}
			// }
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
			journeyDis += Time.deltaTime * speed;
			if (JourneyDelta >= 1)
			{
				Debug.Log("next pos" + journeyLength);
				curMarkerPassed++;
				//startTime = Time.time;
				JourneyDelta -= 1.0f;
				journeyLength = Vector3.Distance(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition);
			}
			// transform.position = Vector3.Lerp(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition, JourneyDelta);
			DesPos = getPoint(JourneyDelta, curMarkerPassed);

			DesPos += (Vector2)Vector3.Cross(path[curMarkerPassed].postition - path[curMarkerPassed + 1].postition, Vector3.back).normalized * ((float)SubLane / (float)SubLaneCnt - 0.5f) * 2.5f;
		}
	}


	float AttackTimer = -1;
	Transform Target;
	Vector3 AttkPos;
	void Update()
	{
		checkAttack();
		subUpdateCauseMyNamesArentGoodEnougthForjim();
	}

	protected void checkAttack()
	{

		if ((FireTimer -= Time.deltaTime) < 0)
		{
			Health target = null;
			float mnD = float.MaxValue;
			Debug.Log(EnemyMask.value);
			foreach (var col in Physics2D.OverlapCircleAll(Trnsfrm.position, range, EnemyMask.value))
			{
				//Debug.Log(col.name);
				float d = (col.transform.position - Trnsfrm.position).sqrMagnitude;
				if (d < mnD)
				{
					d = mnD;
					//Debug.Log("creep close");
					target = col.GetComponent<Health>();
				}
			}
			if (target != null)
			{

				//target->die mutha fucker
				//Debug.Log("creep " +this.name +" pew " +target.name);
				target.hp -= damage;
				FireTimer += rateOfFire;
				otherTimer = rateOfFire + 0.5f;
			}
			else
			{
				FireTimer += 0.15f;
			}
		}
	}

	float Bounce = 0;

    float BounceMod = 1;

    protected void subUpdateCauseMyNamesArentGoodEnougthForjim(){
		if (Bounce < 1.0f)
			Bounce += Time.deltaTime * 4.5f;
		//if(Bounce >1.0f) Bounce -= 1.0f;

        if(Bounce < 1.0f )
            Bounce += Time.deltaTime*4.5f * BounceMod;
        //if(Bounce >1.0f) Bounce -= 1.0f;


		elUpdateDeMove();

        if(Bounce > 1.0f) Bounce = 1.0f;

        Vector3 p = Vector2.Lerp(Trnsfrm.position, DesPos, 5.0f * Time.deltaTime);
       // p.z = p.y + 20.0f;
        p.y += Mathf.Sin(Bounce*Mathf.PI) *0.15f;

        if((AttackTimer-= Time.deltaTime) > 0) {
            if(Target) AttkPos = Target.position;
            float mod = 1.0f-Mathf.Abs(Mathf.Cos(Mathf.PI *(AttackTimer / (rateOfFire*0.2f))));
            p = Vector3.Lerp(p, AttkPos, mod);
            float ang = -35; if(side != 0) ang = -ang;
            Trnsfrm.localEulerAngles = new Vector3(0, 0, ang*mod);
        } else Trnsfrm.localEulerAngles = Vector3.zero;

		Trnsfrm.position = p;

		if (hp <= 0)
		{
			Destroy(gameObject);
		}

	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition);

		Gizmos.DrawWireSphere(transform.position, range);
	}

	public int getCurMarker() { return curMarkerPassed; }
	public float getJournyDelta() { return JourneyDelta; }
}
