using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerHealthBar : MonoBehaviour
{

	public RectTransform GreenBar;
	Tower thisTower;

	// Use this for initialization
	void Start()
	{
		thisTower = this.gameObject.GetComponent<Tower>();
	}

	// Update is called once per frame
	void Update()
	{
		GreenBar.localScale = new Vector3((thisTower.hp / thisTower.maxHP), 1, 1);
	}
}