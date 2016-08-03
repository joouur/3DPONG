using UnityEngine;
using System.Collections;

namespace Pong.Manager
{
    public class AiController : MonoBehaviour
    {

        public float speed = 25f;
        public float negXBound = -3.96f;
        public float posXBound = 3.993f;
        public float negZBound = -3.21f;
        public float posZBound = 3.21f;
        float translationX;
        float translationZ;
        Transform ball;
        Vector3 startPos = new Vector3(-3.094f, -24.15f, 2.801f);
        // Use this for initialization
        void Start()
        {
            
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = startPos;
           
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ball = GameObject.FindGameObjectWithTag("Ball").transform;
            if (ball != null)
            {
                if (ball.position.y < 0)
                {
                    float ballX = ball.position.x;
                    float ballz = ball.position.z;
                    Vector3 pos = transform.position;
                    pos.x = Mathf.Clamp(ballX, negXBound, posXBound);
                    pos.z = Mathf.Clamp(ballz, negZBound, posZBound);
                    transform.position = transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
                }
            }
            else
            {
                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(0.43f, negXBound, posXBound);
                pos.z = Mathf.Clamp(0.32f, negZBound, posZBound);
                transform.position = transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
            }
            




        }
    }
}
