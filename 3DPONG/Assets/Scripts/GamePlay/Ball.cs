using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float speed;
    public bool useTorque;
    public float MaxAngularVelocity = 25;
    private Rigidbody BallRb;

    private Vector3 moveTo;
    private Vector3 moveFrom;
    public void Start()
    {
        moveTo = Vector3.right;
        moveFrom = Vector3.forward;
        BallRb = GetComponent<Rigidbody>();
        BallRb.maxAngularVelocity = MaxAngularVelocity;
        BallRb.useGravity = false;
        BallRb.AddForce(Vector3.one * speed);
    }
	// Update is called once per frame
	public void FixedUpdate () 
    {
        
        //Vector3 pos = new Vector3(0, 0, 0);
	    if(BallRb.maxAngularVelocity != MaxAngularVelocity)
            BallRb.maxAngularVelocity = MaxAngularVelocity;
       // Movement(moveTo, moveFrom);
    }

    private void Movement(Vector3 m, Vector3 u)
    {
        if (useTorque)
        {
            BallRb.AddRelativeTorque(m * speed * Time.deltaTime);
            BallRb.AddRelativeForce((u * Random.Range(-1.0f, 1.0f)) * speed * Time.deltaTime);
        }
        else
            BallRb.AddForce(new Vector3(m.z, m.y, m.x));
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "Player")
        {
            moveTo = Vector3.right;
            moveFrom = Vector3.forward;
            //MaxAngularVelocity += 1.5f;
           // speed += 1.0f;
        }
        else if(other.collider.tag == "Enemy")
        {
            moveTo = Vector3.left;
            moveFrom = Vector3.back;
           // MaxAngularVelocity += 1.5f;
           // speed += 1.0f;

        }
    }
}
