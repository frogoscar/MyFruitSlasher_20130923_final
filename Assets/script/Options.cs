using UnityEngine;
using System.Collections;

public class Options: MonoBehaviour {
	public static bool useMouse;
	
	private string msg;
	public bool pause;  // For Pause/Resume the Game
	public static PXCUPipeline.Mode mode=PXCUPipeline.Mode.GESTURE;
	
	void Awake() {
		Application.targetFrameRate = 30;
	}
	
	void OnGUI() {
		if (GUI.Button (new Rect (10,10,120,30), useMouse ? "USE MOUSE" : "USE GESTURE")) {
			useMouse = !useMouse;
		}
		if (GUI.Button (new Rect (10,40,120,30), "RESTART")) {
			Application.LoadLevel(0);
		}
		if (GUI.Button (new Rect (10,70,120,30), pause ? "RESUME" : "PASUE")) {
			pause = !pause;
			if (pause) {
				Time.timeScale = 0;
			}
			else {
				Time.timeScale = 1;
			}
		}
		if (GUI.Button (new Rect (10,100,120,30),((mode&PXCUPipeline.Mode.VOICE_RECOGNITION)!=0) ? "VOICE" : "NO VOICE")) {
			mode^=PXCUPipeline.Mode.VOICE_RECOGNITION;
			Application.LoadLevel(1);
		}
		
		GUI.Label(new Rect(10, 130, 350, 420), 
			msg!=null?msg:"Instructions: Show one of your hands to the camera, " +
			"and use \"closure + openness of Thumb Finger and Index Finger\" to create the KnifeRay to cut the fruits.\n\n" +
			"GoldApple gives you 20 points, while other fruit gives 1 point.\n" +
			"Accoring to the different angles of you KnifeRay, there are three Splashs." +
			"Every object has a different color of splash.\n\n" +
			"Challenge: Cutting the Fruit and Hamster with Gesture is a bit difficult.\n\n" +
			"Tips: Don't cut the Hamster, it will cause you lose 1 life.");
		
	}
	
	public void SetMessage(string msg2) {
		msg=msg2;
	}
}
