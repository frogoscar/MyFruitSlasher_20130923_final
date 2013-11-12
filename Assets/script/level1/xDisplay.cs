using UnityEngine;
using System.Collections;

public class xDisplay : MonoBehaviour {
	
	public float x;
	public float y;
	
	public float scale = 1;
	
	public Color myColor;
	
	public Texture xTexture;
	
	//private int index = 0;
	private int width = 55;
	private int height = 66;
	
	void OnGUI() {
		GUI.color = myColor;
		GUI.DrawTexture(new Rect(x, y, scale * width, scale * height), xTexture);
	}
}
