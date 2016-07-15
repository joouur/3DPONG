using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

    public float speed = 40f;
    float translationX; // movement of paddle in x direction
    float translationY; // movement of paddle in y direction
    public float translationThrust;
    public float negXBound = -3.975f;
    public float posXBound = 4f;
    public float negZBound = -3.246f;
    public float posZBound = 3.17f;
    public float smoothFactor = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody pdRb;
    private Vector3 startPause;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        pdRb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().isKinematic = true;
        //gameObject.GetComponent<Renderer>().material.color = 0;
        startPause = transform.position;
    }
	
    IEnumerator thrust()
    {
        /*
        if (startPause.y + translationThrust <= transform.position.y)
            pdRb.MovePosition(Vector3.down + transform.position * 4.0f * Time.deltaTime);

        else
            yield return null; 
        */

        //Vector3 pos = transform.position;
        //pos.y += translationThrust;
        //pos.y = Mathf.Clamp(pos.x + translationThrust, 18, 24);
        //transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * .5f);

        //pdRb.AddRelativeForce(Vector3.down * translationThrust, ForceMode.Impulse);
        Vector3 pos = transform.position;
        //pos.y = Mathf.Clamp(pos.y + translationThrust, 24f, 18f);
        pos.y += translationThrust*1.2f;
        pdRb.MovePosition(transform.position - pos * Time.deltaTime);
        yield return new WaitForEndOfFrame();
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
        if (Input.GetMouseButtonDown(0))
            StartCoroutine("thrust");
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + translationX, negXBound, posXBound);
        pos.z = Mathf.Clamp(pos.z + translationY, negZBound, posZBound);
        //transform.position = Vector3.SmoothDamp(transform.position, pos, ref speed, smoothFactor);
        //transform.position = pos;
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
    }
}
