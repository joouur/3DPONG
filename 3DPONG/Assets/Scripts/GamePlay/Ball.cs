using UnityEngine;
using System.Collections;
using Pong.UI;

public class Ball : MonoBehaviour {

    public Vector3 speed;
    public Vector3 vel;
    public float startSpeed;
    [HideInInspector]
    public Rigidbody BallRb;

    public float magnitude;
    private Vector3 grav;
    public float maxAngularSpeed;
    public void Start()
    {
        BallRb = GetComponent<Rigidbody>();
        //BallRb.useGravity = false;
        //BallRb.AddRelativeForce(Vector3.one * 500);
        if (startSpeed == 0)
            startSpeed = -15.0f;
        BallRb.maxAngularVelocity = maxAngularSpeed;
        BallRb.AddRelativeTorque(Vector3.one);
        BallRb.velocity = new Vector3(0, startSpeed, 0);
        grav = new Vector3(0, -9.81f, 0);
    }
    // Update is called once per frame
    public void FixedUpdate()
    {
        magnitude = BallRb.velocity.magnitude;
        ScoreUI.Instance.SetSpeed(magnitude);
    }


    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" || other.collider.tag == "Enemy")
        {
            if (other.collider.tag == "Player")
                ScoreUI.Instance.SetBounces();
            Physics.gravity = grav;
            grav = grav * -1.0f;
            speed = BallRb.velocity;
            if (!(BallRb.velocity.magnitude > 100))
            {
                speed *= 1.025f;
                //Debug.Log(BallRb.velocity.magnitude);
                BallRb.velocity = speed;

            }
            vel = BallRb.velocity;
            magnitude = BallRb.velocity.magnitude;
            ScoreUI.Instance.SetSpeed(magnitude);
        }
    }
}
