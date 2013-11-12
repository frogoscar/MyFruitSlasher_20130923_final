using UnityEngine;
using System.Collections;

public class DigitDisplay : MonoBehaviour {
	
	public string myStringScore;
	
	public float x;
	public float y;
	
	public float scale = 1;
	
	public Color myColor;
	
	public Texture2D[] myNumber = new Texture2D[10];
	
	//private int index = 0;
	private int width = 25;
	private int height = 30;
	
	void OnGUI() {
		GUI.color = myColor;
		
		if (myStringScore != null) {
			for (int i = 0; i < myStringScore.Length; ++i) {
				GUI.DrawTexture(new Rect(x + i * scale * width, y, scale * width, scale * height), myNumber[(int)char.GetNumericValue(myStringScore[i])]);
			}
		}
	}
}
