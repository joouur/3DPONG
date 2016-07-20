using UnityEngine;
using System.Collections;

public class PaddleContact : MonoBehaviour
{

    public Ball ball;
    public bool POrAI;

    public void Start()
    {
        if (!ball)
            ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
    }
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "RollerBall")
        {
            ball.BallRb.velocity = compVelocity(-other.contacts[0].normal, ball.speed);

            /*
                if (POrAI)
                    ball.BallRb.velocity = compVelocity(-other.contacts[0].normal, ball.speed);
                else
                    ball.BallRb.velocity = -compVelocity(-other.contacts[0].normal, ball.speed);
    */
        }
    }
    
    public Vector3 compVelocity(Vector3 normal, Vector3 velo)
    {
        Vector3 cTangnt = new Vector3((normal.x * Mathf.Cos(1.5707f) - normal.y * (float)Mathf.Sin(1.5707f)),
                                    (normal.x * (float)Mathf.Sin(1.5707f) + normal.y * (float)Mathf.Cos(1.5707f)),
                                    (normal.z * Mathf.Cos(1.5707f) - normal.y * (float)Mathf.Sin(1.5707f)));
        float a = -Vector3.Dot(velo, normal);
        float b = Vector3.Dot(velo, cTangnt);
        Vector3 resultantVel = a * normal + b * cTangnt;
        return resultantVel;
    }
}