using UnityEngine;
using System.Collections;

public class Level1ScoreGUI : MonoBehaviour {
	
	public GUISkin mySkin;
	public Texture xTexture;
	public Texture kiwiTexture;
	public Color myColor;
	
	void OnGUI() {
		GUI.skin = mySkin;
		GUI.color = myColor;
		
		GUI.DrawTexture(new Rect(469, 77, 35, 42), xTexture);
		GUI.DrawTexture(new Rect(408, 73, 62, 56), kiwiTexture);
	}
}
