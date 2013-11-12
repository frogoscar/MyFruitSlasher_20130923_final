using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour {
	
	// Indicate the life left
	public static int life;
	
	// The five life cross
	public GameObject firstX;
	public GameObject secondX;
	public GameObject thirdX;
	public GameObject fourthX;
	public GameObject fifthX;
	
	public Color color;
	
	// Update is called once per frame
	void Update () {
		switch (life) {
			case 5:
				firstX.transform.GetComponent<xDisplay>().myColor = color;
			 	break;
			case 4:
				secondX.transform.GetComponent<xDisplay>().myColor = color;
				break;
			case 3:
				thirdX.transform.GetComponent<xDisplay>().myColor = color;
				break;
			case 2:
				fourthX.transform.GetComponent<xDisplay>().myColor = color;
				break;
		}
	}
}
