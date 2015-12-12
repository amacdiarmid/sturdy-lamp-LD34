using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepAI : MonoBehaviour {

    //path staff
    public laneMarker lane;
    public List<pathMarker> path;
    //float startTime;
    float JourneyDelta;
    float journeyLength;
    int curMarkerPassed = 0;
    public float speed = 1;
    [Range(0, 1)] float attackCheck = 0.1f;
    float lastCheck;
    public bool move;
    [HideInInspector] public float distance;
    [HideInInspector] public CreepAI Next;
    [HideInInspector] public CreepAI Prev;
    //attack stuff
    public float damage;
    public float range;
    public float hp;

    // Use this for initialization
    void Start ()
    {
        //startTime = Time.time;
        JourneyDelta = 0;
        //Debug.Log(path[curMarkerPassed].postition + " " + path[curMarkerPassed + 1].postition);
        journeyLength = Vector3.Distance(path[curMarkerPassed].transform.position, path[curMarkerPassed + 1].transform.position);
        //Debug.Log("length" + journeyLength);
        move = true;
    }
    struct CubicSpline {
        public CubicSpline( Vector2 a, Vector2 b, Vector2 c, Vector2 d ) {

            M0 = 0.5f * d - 1.5f * c - 0.5f * a + 1.5f * b;
            M1 = a - 2.5f * b + 2.0f * c - 0.5f * d;
            M2 = 0.5f * c - 0.5f * a;
            M3 = b;


        }
        public Vector2 get( float delta ) {
            float delta2 = delta * delta;
            return M0 * delta * delta2 + M1 * delta2 + M2 * delta + M3;
        }
        Vector2 M0, M1, M2, M3;
    };

    private Vector2 getPoint( float t, int i2 ) {
        int i1 = i2-1, i3 = i2+1, i4 = i2+2;
        Vector2 p2 = path[i2].postition, p3 = path[i3].postition, p1, p4;
        if(i1 < 0) p1 = p2+p2-p3;
        else p1 = path[i1].postition;
        if(i4 >= path.Count ) p4 = p3+p3-p2;
        else p4 = path[i4].postition;

        //t *= 0.25f; t+= 0.25f;
        CubicSpline s = new CubicSpline(p1, p2, p3, p4);
        return s.get(t);
    }

	// Update is called once per frame
	void Update ()
    {
        if (move == true)
        {
            distance = Vector3.Distance(transform.position, path[curMarkerPassed + 1].transform.position);
            for (int i = curMarkerPassed + 1; i < path.Count; i++)
            {
                distance += Vector3.Distance(path[i - i].transform.position, path[i].transform.position);
            }

            //float distCovered = (Time.time - startTime) * speed;
            //float fracJourney = distCovered / journeyLength;
            //transform.position = Vector3.Lerp(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition, fracJourney);
            //Debug.Log(fracJourney);
            JourneyDelta += Time.deltaTime*speed / journeyLength;
            if(JourneyDelta >= 1)
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
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(path[curMarkerPassed].postition, path[curMarkerPassed + 1].postition);
    }
}
