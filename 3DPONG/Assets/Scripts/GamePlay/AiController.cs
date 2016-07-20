using UnityEngine;
using System.Collections;

public class AiController : MonoBehaviour {

    public float speed = 25f;
    public float negXBound = -3.96f;
    public float posXBound = 3.993f;
    public float negZBound = -3.21f;
    public float posZBound = 3.21f;
    float translationX;
    float translationZ;
    Transform ball;
    // Use this for initialization
    void Start () {  
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
        GetComponent<Rigidbody>().isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
       
        if (ball.position.y < 0)
        {
            float ballX = ball.position.x;
            float ballz = ball.position.z;
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(ballX, negXBound, posXBound);
            pos.z = Mathf.Clamp(ballz, negZBound, posZBound);
            transform.position = transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime*speed);
        }
        



    }
}
