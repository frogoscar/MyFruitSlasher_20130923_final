using UnityEngine;
using System.Collections;

public class HitByKnife : MonoBehaviour {

	public bool isClicked = false;
	KnifeRay3D knifeRay3D= null;  // For calling the script KnifeRay3D.cs
	
	public GameObject goldFruit;
	public AudioClip fallSound;
	public AudioClip hamsterSound;
	
	public GameObject xRed;
	
	private GameObject myKnifeScript;
	private GameObject myGoldFruit;
	
	public static int myScore = 0;
	public static int life = 6;
	
	private GameObject myxRed;
	
	public ShowHand showHand;
	
	// Update is called once per frame
	void Update () {
		if (Options.useMouse) {
			bool isMouseDown = Input.GetMouseButton(0);
			if (!isClicked) {
				if (isMouseDown) {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if (collider.Raycast(ray, out hit, 100.0f)) {
						isClicked = true;
						knifeRay3D = gameObject.GetComponent<KnifeRay3D>();
						knifeRay3D.isHit = true;
						knifeRay3D.rayPos = hit.transform.position;
					}
				}
			}
			else if (gameObject.GetComponent<KnifeRay3D>().isRay) {
				if (gameObject.name != "Hamster00(Clone)") {
					if (gameObject.name == "GoldApple00(Clone)") {
						myGoldFruit = (GameObject)Instantiate(goldFruit, transform.position, Quaternion.identity);
						Destroy(myGoldFruit, 1.0f);
						myScore += 20;
					}
					else {
						myScore++;
					}
					Score.score = myScore;
					gameObject.SetActive(false);
					Destroy(gameObject);
				}
				else {
					if (audio.isPlaying) {
						audio.Stop();
					}
					PlaySound(hamsterSound);
					gameObject.SetActive(false);
					Destroy(gameObject);
					
					life--;
					LifeController.life = life;
				
					if (life == 1) {
						Application.LoadLevel(2);
					}
				}	
			}
		}
		else {
			bool isMouseDown = ShowHand.isMouseDown;
			if (!isClicked) {
				if (isMouseDown) {
					// Adjustment of the RayPosition according to the Fruit Creation Position
					Vector3 rayWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(ShowHand.pHandMiddle.x, ShowHand.pHandMiddle.y, 1));
					
					float temp = rayWorldPos.x;
					if (temp > -1.5f && temp < -1.3f) {
						rayWorldPos.x = (float)(-0.63f + (-0.9f - temp) * 2.0f);
					}
					else if (temp >= -1.3f && temp <= -1.24f) {
						rayWorldPos.x = (float)(-0.63f + (-0.9f - temp) * 4.0f);
					}
					else if (temp > -1.24f && temp < -0.8f) {
						rayWorldPos.x = (float)(-0.63f + (-0.9f - temp) * 5.0f);
					}
					
					Vector3 rayImagePos = Camera.main.WorldToScreenPoint(rayWorldPos);
	
					Ray ray = Camera.main.ScreenPointToRay(new Vector3(rayImagePos.x, rayImagePos.y, 1));
					RaycastHit hit;
					if (collider.Raycast(ray, out hit, 100.0f)) {
						isClicked = true;
						knifeRay3D = gameObject.GetComponent<KnifeRay3D>();
						knifeRay3D.isHit = true;
						knifeRay3D.rayPos = hit.transform.position;
					}
				}
			}
			else if (gameObject.GetComponent<KnifeRay3D>().isRay) {
				if (gameObject.name != "Hamster00(Clone)") {
					if (gameObject.name == "GoldApple00(Clone)") {
						myGoldFruit = (GameObject)Instantiate(goldFruit, transform.position, Quaternion.identity);
						Destroy(myGoldFruit, 1.0f);
						myScore += 20;
					}
					else {
						myScore++;
					}
					Score.score = myScore;
					gameObject.SetActive(false);
					Destroy(gameObject);
				}
				else {
					if (audio.isPlaying) {
						audio.Stop();
					}
					PlaySound(hamsterSound);
					gameObject.SetActive(false);
					Destroy(gameObject);
					
					life--;
					LifeController.life = life;
				
					if (life == 1) {
						Application.LoadLevel(2);
					}
				}	
			}
		}
		
		if (gameObject.transform.position.y <= -0.55f && gameObject.name != "Hamster00(Clone)") {
			
			myxRed = (GameObject)Instantiate(xRed, new Vector3(0, 0, -1.9f), Quaternion.identity);
			myxRed.transform.GetComponent<xDisplay>().x = Camera.main.WorldToScreenPoint(transform.position).x;
			myxRed.transform.GetComponent<xDisplay>().y = 605;
			
			if (audio.isPlaying) {
				audio.Stop();
			}
			PlaySound(fallSound);
			Destroy(gameObject);
			
			life--;
			LifeController.life = life;
			
			if (life == 1) {
				Application.LoadLevel(2);
			}
			
			Destroy(myxRed, 1.5f);
		}
	}
	
	void PlaySound(AudioClip sound) {
		if (!audio.isPlaying) {
			AudioSource.PlayClipAtPoint(sound, new Vector3(0, 0, -1.9f));
		}
	}
}
