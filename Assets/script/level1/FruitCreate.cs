using UnityEngine;
using System.Collections;

public class FruitCreate : MonoBehaviour {
	
	public GameObject[] fruit = new GameObject[7];
	public int index;
	private GameObject myFruit;
	
	void Start () {
		Physics.gravity = new Vector3(0, -1.5f, 0);
		InvokeRepeating("Move", 2, 4);  // Every 4 seconds do the "Move" function to generate a fruit or a hamster
	}
	
	void Move() {
		//float x = Random.Range(-0.63f, 0.0f);
		//float x = Random.Range(0.0f, 0.55f);
		float x = Random.Range(-0.63f, 0.55f);
		index = Random.Range(0, 7);
		
		myFruit = (GameObject)Instantiate(fruit[index], new Vector3(x, -0.2f, -0.9f), Quaternion.identity);
		
		if (x > 0) {
			//Random.Range(-1, 0)
			myFruit.transform.rigidbody.velocity = new Vector3(0, 1, 0);
		}
		else {
			myFruit.transform.rigidbody.velocity = new Vector3(0, 1, 0);
		}
		
		Destroy(myFruit, 3);
	}
}
