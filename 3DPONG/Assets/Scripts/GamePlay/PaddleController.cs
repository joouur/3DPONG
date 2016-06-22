using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

    public float speed = 50f;
    float translationX; // movement of paddle in x direction
    float translationY; // movement of paddle in y direction
    public float negXBound = -4.4f;
    public float posXBound = 4.4f;
    public float negYBound = -1.8f;
    public float posYBound = 4.8f;
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

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + mouseX, negXBound, posXBound);
        pos.y = Mathf.Clamp(pos.y + mouseY, negYBound, posYBound);
        transform.position = pos;
    }
}
