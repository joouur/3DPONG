using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

    public float speed = 50f;
    float translationX; // movement of paddle in x direction
    float translationY; // movement of paddle in y direction
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //these two translations get the mouse position and multiply it by a set speed
        //it is then multiplied by timedelta and then the translation is made.
        translationX = Input.GetAxis("Mouse X") * speed; 
        translationY = Input.GetAxis("Mouse Y") * speed;
        translationX *= Time.deltaTime;
        translationY *= Time.deltaTime;
        transform.Translate(translationX, translationY, 0f);
    }
}
