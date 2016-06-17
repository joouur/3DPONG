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
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        translationX =  mouseX * speed; 
        translationY =  mouseY * speed;
        translationX *= Time.deltaTime;
        translationY *= Time.deltaTime;
        if ((translationX >= -4.4 && translationX <=4.4) && 
            (translationY >= -1.8 && translationY <= 4.8))
        {

        }
        transform.Translate(translationX, translationY, 0f);
    }
}
