using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

    public float speed = 40f;
    float translationX; // movement of paddle in x direction
    float translationY; // movement of paddle in y direction
    public float translationThrust = 10f;
    public float paddleHome = 24.01f;
    public float negXBound = -3.96f;
    public float posXBound = 3.993f;
    public float negZBound = -3.21f;
    public float posZBound = 3.21f;
    public float smoothFactor = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody pdRb;
    private Vector3 startPause;
    public float thrustSpeed = 1.2f;
    public bool thrustEnabled = true;
    //public bool thrustKey = Input.GetMouseButtonDown(0);
	// Use this for initialization
	void Start () {
        //Cursor.visible = false;
        pdRb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().isKinematic = true;
        //gameObject.GetComponent<Renderer>().material.color = 0;
        startPause = transform.position;
    }
	
    IEnumerator thrust()
    {
        //thrustEnabled = false;
        //Vector3 pos = transform.position;
        //pos.y = Mathf.Clamp(pos.y - translationThrust*thrustSpeed, 24.01f, 21f);
        //pos.y += translationThrust*thrustSpeed;
        pdRb.AddForce(Vector3.down*Time.deltaTime*thrustSpeed);
        //new WaitForSeconds(4);
        //pos.y -= translationThrust * thrustSpeed;
        //pdRb.MovePosition(transform.position + pos * Time.deltaTime);
        //StartCoroutine("returnFromThrust");
        yield return new WaitForEndOfFrame();
    }
    
    
    
	// Update is called once per frame
	void Update () {
        //these two translations get the mouse position and multiply it by a set speed
        //it is then multiplied by timedelta and then the translation is made.
        Vector3 pos = transform.position;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        translationX =  mouseX * speed; 
        translationY =  mouseY * speed;
        translationX *= Time.deltaTime;
        translationY *= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("thrust");
        }
            
        
        pos.x = Mathf.Clamp(pos.x + translationX, negXBound, posXBound);
        pos.z = Mathf.Clamp(pos.z + translationY, negZBound, posZBound);
        //transform.position = Vector3.SmoothDamp(transform.position, pos, ref speed, smoothFactor);
        //transform.position = pos;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
    }
}
