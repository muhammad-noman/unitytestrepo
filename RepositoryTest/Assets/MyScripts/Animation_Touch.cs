using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Animation_Touch : MonoBehaviour {
	public bool j, rot, x, d, batwhale; // for revolving around centre, rotation button working,
	public string AnimName;
	public int animAvail;
	public AudioClip alphabet, animalName, animSound1, animSound2, animSound3, animSound4, animDefSound;
	public string Anim1, Anim2, Anim3, default_anim;//Animation string names
	public float x_ax, y_ax, z_ax; //rotation values
	public float upr_lmt, lwr_lmt; //scaling limits lossy scale
	public float upr_l_lmt, lwr_l_lmt; //scaling limits local scale
	public float scl_speed;
	Vector3 default_scale, default_pos;
	Quaternion default_rotation, prnt_rotat;
	GameObject Block, Block2;
	public bool tch,  why; //rotation enable or disable, enables touch animation to play after animation is finished, for button animation finishing

	float timepassed;
	bool animTch;


	// Use this for initialization
	void Start () {
		x = true;
		tch = true;
		Block = this.gameObject;
		d = true;
		default_scale = Block.GetComponent<Transform> ().localScale;
		default_rotation = Block.GetComponent<Transform> ().localRotation;
		default_pos = Block.GetComponent<Transform> ().localPosition;
		prnt_rotat = Block.transform.parent.GetComponent<Transform> ().localRotation;
		animTch = false;
	}
	
	// Update is called once per frame
	void Update () {



		// Touch
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
				//swipe left
				if (touchZero.deltaPosition.x < 0  /* && touchZero.deltaPosition.y > -0.1f  && touchZero.deltaPosition.y < 0.1f */) {
					Debug.Log ("left swipe");
					this.gameObject.transform.Rotate (Vector3.up * 100f * Time.deltaTime);

				}
				//swipe right
				if (touchZero.deltaPosition.x > 0  /* && touchZero.deltaPosition.y > -0.1f  && touchZero.deltaPosition.y < 0.1f */) {
					Debug.Log ("right swipe");
					this.gameObject.transform.Rotate (Vector3.down * 100f * Time.deltaTime);

				}

			}
			if (Input.GetTouch (0).phase == TouchPhase.Began && Input.GetTouch (0).phase != TouchPhase.Moved) {
				if (EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId) == false) {
					// playing animation with touch
					Debug.Log ("Touch not on UI");
					Touch touch = Input.GetTouch (0);
					Ray ray = Camera.main.ScreenPointToRay (touch.position);
					RaycastHit hit;
					default_anim = this.gameObject.GetComponent<Animation> ().clip.name;
					if (Physics.Raycast (ray, out hit, 100f) && why == true) {
						animTch = true;
					}
				}
			}
				

		} else if (Input.touchCount == 2) {
			timepassed = 1f;
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			if (deltaMagnitudeDiff < 0f && this.gameObject.transform.lossyScale.x < upr_lmt) {
				this.gameObject.transform.localScale += new Vector3 (scl_speed, scl_speed, scl_speed) * Time.deltaTime;
			}
			if (deltaMagnitudeDiff > 0f && this.gameObject.transform.lossyScale.x > lwr_lmt) {
				this.gameObject.transform.localScale -= new Vector3 (scl_speed, scl_speed, scl_speed) * Time.deltaTime;
			}


		}
		else {
			if (animTch == true && timepassed <=0.5f) {
				why = false;
				StartCoroutine (Anim ());
			}
			timepassed = 0f;
			animTch = false;
		}
			
	}



	IEnumerator Anim ()
	{
		this.GetComponent<Animation> ().Stop();
		this.GetComponent<Animation>().Play(AnimName);
		this.GetComponent<AudioSource> ().Stop ();
		this.GetComponent<AudioSource> ().PlayOneShot (animSound4);
		yield return new WaitForSeconds (this.GetComponent<Animation>().GetClip(AnimName).length);
		this.GetComponent<AudioSource> ().Stop ();
		this.GetComponent<Animation> ().Play(default_anim);
		this.GetComponent<AudioSource> ().clip = animDefSound;
		this.GetComponent<AudioSource> ().Play ();
		why = true;
		d = true;
		if (j == true){
			this.transform.parent.GetComponent<Animation> ().Stop ();
			Block.GetComponent<Transform> ().localRotation = default_rotation;
			Block.GetComponent<Transform> ().localPosition = default_pos;
			Block.transform.parent.GetComponent<Transform> ().localRotation = new Quaternion (x_ax , y_ax , z_ax, 0);
			Block.transform.localPosition = default_pos;
			x = true;
		}


	}

}
