using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

    public float speed = 50f;
    float translationX; // movement of paddle in x direction
    float translationY; // movement of paddle in y direction
    public float negXBound = -4.4f;
    public float posXBound = 4.4f;
    public float negZBound = -4.15f;
    public float posZBound = 2.63f;
    public float smoothFactor = 0.3f;
    private Vector3 velocity = Vector3.zero;
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
        pos.x = Mathf.Clamp(pos.x + translationX, negXBound, posXBound);
        pos.z = Mathf.Clamp(pos.z + translationY, negZBound, posZBound);
        //transform.position = Vector3.SmoothDamp(transform.position, pos, ref speed, smoothFactor);
        //transform.position = pos;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
    }
}
