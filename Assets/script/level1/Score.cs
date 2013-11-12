using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	// Indicate the score gotten
	public static int score;
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<DigitDisplay>().myStringScore = score.ToString();
	}
}
