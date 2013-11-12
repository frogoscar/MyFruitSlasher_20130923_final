using UnityEngine;
using System.Collections;

public class KnifeRay3D : MonoBehaviour {
	
	public GameObject myRay;
	public AudioClip knifeSound;
	
	public GameObject firstFruit;
	public GameObject secondFruit;
	
	public GameObject splash;
	public GameObject splashH;
	public GameObject splashV;
	
	public Vector3 firstPos;
	public Vector3 secondPos;
	public Vector3 rayPos;
	
	public bool isHit = false;
	public bool isRay = false;
	
	private bool isClicked = false;
	private GameObject rayGameObject;
	private GameObject myFirstFruit;
	private GameObject mySecondFruit;
	private GameObject mySplash;
	private GameObject mySplashH;
	private GameObject mySplashV;
	private float angle;
	
	void Update() {
		//PXCMGesture.GeoNode.Openness.LABEL_CLOSE;
		
		bool isMouseDown = Options.useMouse ? Input.GetMouseButton(0) : ShowHand.isMouseDown;
		
		if (isHit) {
			if (isMouseDown && !isClicked) {
				if (Options.useMouse) {
					firstPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
				}
				else {
					firstPos = Camera.main.ScreenToWorldPoint(new Vector3(ShowHand.pHandMiddle.x, ShowHand.pHandMiddle.y, 1));
				}
				
			}
			
			else if (isMouseDown) {
				if (Options.useMouse) {
					secondPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
				}
				else {
					secondPos = Camera.main.ScreenToWorldPoint(new Vector3(ShowHand.pMiddleThumbIndex.x, ShowHand.pMiddleThumbIndex.y, 1));
				}
			}
			
			else if (Options.useMouse ? Input.GetMouseButtonUp(0) : !ShowHand.isMouseDown) {
				isRay = true;
				if (Options.useMouse) {
					secondPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
				}
				else {
					secondPos = Camera.main.ScreenToWorldPoint(new Vector3(ShowHand.pMiddleThumbIndex.x, ShowHand.pMiddleThumbIndex.y, 1));
				}
				
				if (secondPos.x != firstPos.x) {
					angle = Mathf.Atan((secondPos.y - firstPos.y) / (secondPos.x - firstPos.x));
				}
				else {
					angle = 0;
				}
				
				float angleDegree = angle * 180f / 3.14f;
				
				if ((angleDegree > 30 && angleDegree < 0) || (angleDegree < 0 && angleDegree > -30)) {
					mySplashH = (GameObject)Instantiate(splashH, transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
					SetColor(mySplashH);
					Destroy(mySplashH, 8.0f);
				}
				else if ((angleDegree < 90 && angleDegree > 60) || (angleDegree < -60 && angleDegree > -90)) {
					mySplashV = (GameObject)Instantiate(splashV, transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
					SetColor(mySplashV);
					Destroy(mySplashV, 8.0f);
				}
				else {
					mySplash = (GameObject)Instantiate(splash, transform.position + new Vector3(0.2f, 0, 0), Quaternion.identity);
					SetColor(mySplash);
					Destroy(mySplash, 8.0f);
				}
				
				rayGameObject = (GameObject)Instantiate(myRay, rayPos, Quaternion.AngleAxis(angleDegree, Vector3.forward));
				
				myFirstFruit = (GameObject)Instantiate(firstFruit, transform.position, Quaternion.AngleAxis(Random.Range(1, 3) * 180.0f / 3.14f, Vector3.forward));
				mySecondFruit = (GameObject)Instantiate(secondFruit, transform.position, Quaternion.AngleAxis(Random.Range(1, 3) * 180.0f / 3.14f, Vector3.forward));
				
				if (Random.Range(1, 10) > 5) {
					myFirstFruit.rigidbody.velocity = new Vector2(0.8f, 1.0f);
					mySecondFruit.rigidbody.velocity = new Vector2(-0.9f, -1.0f);
				}
				else {
					myFirstFruit.rigidbody.velocity = new Vector2(-0.8f, 1.0f);
					mySecondFruit.rigidbody.velocity = new Vector2(0.9f, -1.0f);
				}
				
				Physics.gravity = new Vector3(0, -1.5f, 0);
				
				Destroy(myFirstFruit, 2.0f);
				Destroy(mySecondFruit, 2.0f);
				
				if (audio.isPlaying) {
					audio.Stop();
				}
				
				PlaySound(knifeSound);
				Destroy(rayGameObject, 0.2f);
				
				isClicked = false;
				isHit = false;	
			}
		}
		else {
			isRay = false;
		}
	}
	
	void PlaySound(AudioClip sound) {
		if (!audio.isPlaying) {
			AudioSource.PlayClipAtPoint(sound, new Vector3(0, 0, -1.9f));
		}
	}
	
	void SetColor(GameObject myGameObject) {
		if (gameObject.name == "Apple00(Clone)") {
			myGameObject.transform.renderer.material.color = Color.cyan;
		}
		else if (gameObject.name == "Banana00(Clone)") {
			myGameObject.transform.renderer.material.color = Color.black;
		}
		else if (gameObject.name == "GoldApple00(Clone)") {
			myGameObject.transform.renderer.material.color = Color.yellow;
		}
		else if (gameObject.name == "Hamster00(Clone)") {
			myGameObject.transform.renderer.material.color = Color.red;
		}
		else if (gameObject.name == "Kiwi00(Clone)") {
			myGameObject.transform.renderer.material.color = Color.grey;
		}
		else if (gameObject.name == "Pear00(Clone)") {
			myGameObject.transform.renderer.material.color = Color.white;
		}
		else if (gameObject.name == "WaterMelon00(Clone)") {
			myGameObject.transform.renderer.material.color = Color.green;
		}
	}
}

