using UnityEngine;
using System.Collections;

public class Level1SoundGUI : MonoBehaviour {
	
	public GUISkin mySkin;
	
	private bool isSound1Button = false;
	private bool isSound2Button = true;
	
	private AudioSource sound;
	
	// Use this for initialization
	void Start () {
		sound = (AudioSource)gameObject.GetComponent("AudioSource");
	}
	
	void OnGUI() {
		GUI.skin = mySkin;
		
		if (isSound1Button) {
			if (GUI.Button(new Rect(420, 600, 36, 38), "", "Sound1Button")) {
				sound.Play();
				isSound1Button = false;
				isSound2Button = true;
			}
		}
		
		if (isSound2Button) {
			if (GUI.Button(new Rect(420, 600, 49, 38), "", "Sound2Button")) {
				sound.Stop();
				isSound1Button = true;
				isSound2Button = false;
			}
		}
	}
}
