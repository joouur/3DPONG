using UnityEngine;
using System.Collections;

public class AiController : MonoBehaviour {

    public float speed = 50f;
    public float negXBound = -4.4f;
    public float posXBound = 4.4f;
    public float negZBound = -4.16f;
    public float posZBound = 2.66f;
    float translationX;
    float translationZ;
    Transform ball;
    // Use this for initialization
    void Start () {
        ball = GameObject.Find("RollerBall").transform;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        float ballX = ball.position.x;
        float ballz = ball.position.z;

        translationX = ballX; //* speed;
        translationZ = ballz; //* speed;
        translationX *= Time.deltaTime;
        translationZ *= Time.deltaTime;

        
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + translationX, negXBound, posXBound);
        pos.z = Mathf.Clamp(pos.z + translationZ, negZBound, posZBound);
        transform.position = pos;
        */
        //transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, Time.time);
        if (ball.position.y < 0)
        {
            float ballX = ball.position.x;
            float ballz = ball.position.z;
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(ballX, negXBound, posXBound);
            pos.z = Mathf.Clamp(ballz, negZBound, posZBound);
            transform.position = transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime*.03f);
        }
        



    }
}
