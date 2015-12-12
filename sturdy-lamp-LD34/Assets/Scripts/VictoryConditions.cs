using UnityEngine;
using System.Collections;

public class VictoryConditions : MonoBehaviour {

    public Tower leftMainTower;
    public Tower RightMainTower;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (leftMainTower.hp <= 0)
        {
            victory("right");
        }
        if (RightMainTower.hp <= 0)
        {
            victory("left");
        }

    }

    void victory(string winner)
    {
        //craig do some fancy ui shit here
        Debug.Log("winner is " + winner + "! suck it other team!");
        Time.timeScale = 0;
        //replay menus or quit or some shit like that
    }
}
