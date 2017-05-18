using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUi : MonoBehaviour {
    
    public GameObject TopPanel, MidPanel, BotPanel;
    public string TopAnim, MidAnim, BotAnim;
    public int indx;
    float timepassed;
    bool x, tch;

	// Use this for initialization
	void Start () {
        indx = 0;
        x = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (indx == 1 && x == false)
        {
            TopPanel.SetActive(true);
            TopPanel.GetComponent<Animation>().Play();
            BotPanel.SetActive(false);
            MidPanel.SetActive(false);
        }
        else if (indx == 2 && x == false)
        {
            MidPanel.SetActive(true);
            MidPanel.GetComponent<Animation>().Play();
            TopPanel.SetActive(false);
            BotPanel.SetActive(false);
        }
        else if (indx == 3 && x == false)
        {
            BotPanel.SetActive(true);
            BotPanel.GetComponent<Animation>().Play();
            TopPanel.SetActive(false);
            MidPanel.SetActive(false);
        }
        
        if (Input.touchCount == 1 && indx != 0) {
            timepassed += Time.deltaTime;
            // rotating object with touch
            if (tch == true && Input.GetTouch (0).phase == TouchPhase.Moved) {
                //this.gameObject.GetComponent<BoxCollider> ().enabled = false;
                Touch touchZero = Input.GetTouch (0);
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                //swipe upwards
                if (touchZero.deltaPosition.y > 0 && touchZero.deltaPosition.x > -0.1f && touchZero.deltaPosition.x < 0.1f) {
                    Debug.Log ("up swipe");
                    if (indx < 3)
                        indx++;
                }
                //swipe down
                if (touchZero.deltaPosition.y < 0 && touchZero.deltaPosition.x > -0.1f && touchZero.deltaPosition.x < 0.1f) {
                    Debug.Log ("down swipe");
                    if (indx > 1)
                        indx--;
                }
            }
        }
	}

    public void OnPressTop()
    {
        TopPanel.GetComponent<Animation>().Play();
        indx = 1;
        BotPanel.SetActive(false);
        MidPanel.SetActive(false);
        x = true;
    }

    public void OnPressMid()
    {
        MidPanel.GetComponent<Animation>().Play();
        indx = 2;
        TopPanel.SetActive(false);
        BotPanel.SetActive(false);
        x = true;
    }
    public void OnPressBot()
    {
        BotPanel.GetComponent<Animation>().Play();
        indx = 3;
        TopPanel.SetActive(false);
        MidPanel.SetActive(false);
        x = true;
    }
}






