using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public Vector3 speed;
    private Rigidbody BallRb;
    public float maxAngularSpeed;
    public void Start()
    {
        BallRb = GetComponent<Rigidbody>();
        BallRb.useGravity = false;
        BallRb.AddRelativeForce(Vector3.one * 500);
        BallRb.maxAngularVelocity = maxAngularSpeed;
    }
    // Update is called once per frame
    public void FixedUpdate()
    {
        Ray ray = new Ray(this.gameObject.transform.position, BallRb.velocity * 15.0f);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.name =="Player" || hit.collider.gameObject.name == "Enemy")
            {
                transform.Rotate(Vector3.one);
                speed = BallRb.velocity;
            }
        }
    }

    private void Movement(Vector3 m, Vector3 u)
    {
    }

    public void OnCollisionEnter(Collision other)
    {

    }
}
