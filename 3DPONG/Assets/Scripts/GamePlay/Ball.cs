using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float speed;
    public bool useTorque;
    public float MaxAngularVelocity = 25;
    private Rigidbody BallRb;

    private Vector3 moveTo;

    public void Start()
    {
        moveTo = Vector3.right;
        BallRb = GetComponent<Rigidbody>();
        BallRb.maxAngularVelocity = MaxAngularVelocity;
    }
	// Update is called once per frame
	public void LateUpdate () 
    {
        
        //Vector3 pos = new Vector3(0, 0, 0);
	    if(BallRb.maxAngularVelocity != MaxAngularVelocity)
            BallRb.maxAngularVelocity = MaxAngularVelocity;
        Movement(moveTo);
    }

    private void Movement(Vector3 m)
    {
        if (useTorque)
        {
            BallRb.AddTorque(m * speed);
        }
        else
            BallRb.AddForce(new Vector3(m.z, m.y, m.x));
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            moveTo = Vector3.right;
            MaxAngularVelocity += 1.5f;
            speed += 1.0f;
        }
        else if(other.collider.tag == "Enemy")
        {
            moveTo = Vector3.left;
            MaxAngularVelocity += 1.5f;
            speed += 1.0f;

        }
    }
}
