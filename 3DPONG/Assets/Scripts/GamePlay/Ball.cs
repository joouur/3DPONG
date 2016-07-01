using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public Vector3 speed;
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
        Ray ray = new Ray(this.gameObject.transform.position, BallRb.velocity * 15.0f);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag =="Player" || hit.collider.tag == "Enemy")
            {
                //transform.Rotate(Vector3.one);
                speed = BallRb.velocity;
                if (!(BallRb.velocity.magnitude > 90))
                {
                    speed *= 1.025f;
                    //Debug.Log(BallRb.velocity.magnitude);
                }
                magnitude = BallRb.velocity.magnitude;
            }
        }
        
    }

    private void Movement(Vector3 m, Vector3 u)
    {
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" || other.collider.tag == "Enemy")
        {
            Physics.gravity = grav;
            grav = grav * -1.0f;
        }
    }
}
