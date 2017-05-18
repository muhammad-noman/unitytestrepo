using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Animation_tounch : MonoBehaviour {

	public string Anim1, Anim2, Anim3, default_anim;
	bool animTch;
	public bool tch,  why;
	float timepassed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.touchCount == 1) {
			timepassed += Time.deltaTime;
			// rotating object with touch
			if (tch == true && Input.GetTouch (0).phase == TouchPhase.Moved) {
				//this.gameObject.GetComponent<BoxCollider> ().enabled = false;
				Touch touchZero = Input.GetTouch (0);
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				//swipe upwards
				if (touchZero.deltaPosition.y > 0 && touchZero.deltaPosition.x > -0.1f && touchZero.deltaPosition.x < 0.1f) {
					Debug.Log ("up swipe");
				}
				//swipe down
				if (touchZero.deltaPosition.y < 0 && touchZero.deltaPosition.x > -0.1f && touchZero.deltaPosition.x < 0.1f) {
					Debug.Log ("down swipe");
				}
			}
		}

	}
		

}
