using UnityEngine;
using System.Collections;

public class start : MonoBehaviour {
	
	private bool isSound1Button = false;
	private bool isSound2Button = true;
	private AudioSource sound;
	public GUISkin mySkin;
	
	// Scale factor
	private float fScaleWidth;
	private float fScaleHeight;
	
	void Awake() {
		fScaleWidth = (float)(Screen.width)/640;
		fScaleHeight = (float)(Screen.height)/480;
	}
	
	void Start () {  
        sound = (AudioSource)gameObject.GetComponent("AudioSource");
 	}
	
	void OnGUI() {
		//GUI.matrix = Matrix4x4.TRS (new Vector3(0, 0, 0), Quaternion.identity, new Vector3(fScaleWidth, fScaleHeight, 1));
		GUI.skin = mySkin;
		
		if (GUI.Button(new Rect(680, 340, 220, 66), "", "PlayButton")) {
			Application.LoadLevel(1);
			Score.score = 0;
			LifeController.life = 6;
			HitByKnife.myScore = 0;
			HitByKnife.life = 6;
		}
		
		if (GUI.Button(new Rect(630, 420, 320, 66), "", "MoreButton")) {
			Application.OpenURL("http://bbs.51cto.com/forumdisplay.php?fid=153&filter=0&orderby=dateline&ascdesc=DESC");
		}
		
		if (GUI.Button(new Rect(700, 500, 179, 66), "", "CreditButton")) {
			Application.LoadLevel(0);
		}
		
		if (isSound1Button) {
			if (GUI.Button(new Rect(500, 560, 20, 31), "", "Sound1Button")) {
				sound.Play();
				isSound1Button = false;
				isSound2Button = true;
			}
		}
		
		if (isSound2Button) {
			if (GUI.Button(new Rect(500, 560, 37, 31), "", "Sound2Button")) {
				sound.Stop();
				isSound1Button = true;
				isSound2Button = false;
			}
		}
	}
}
