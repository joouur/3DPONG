using UnityEngine;
using System.Collections;

namespace Pong.Gameplay
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
        float hitModifier = 0f;
        bool checkRand = false;
        float lrEdge = 2.11f;
        float udEdge = 1.27f;
        private Rigidbody aiRb;
        float edgeSensitivity = 20f;
        public float thrustSpeed = 10f;
        public bool thrustEnabled = false;
        public bool isHit = true;
        Transform ball;
        Vector3 startPos = new Vector3(-3.094f, -24.15f, 2.801f);
        // Use this for initialization
        void Start()
        {
            aiRb = GetComponent<Rigidbody>();
            aiRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = startPos;
            StartCoroutine("noThrustOnStart");
           
        }
        IEnumerator thrust()
        {
            Vector3 pos = new Vector3(0f, 40f, 0f);
            if (thrustEnabled)
            {
                aiRb.MovePosition(transform.position + pos * Time.deltaTime);
                thrustEnabled = false;
            }   
            yield return new WaitForSeconds(0.2f);
            aiRb.MovePosition(new Vector3(transform.position.x, startPos.y, transform.position.z));
            thrustEnabled = true;
        }
        IEnumerator noThrustOnStart()
        {
            thrustEnabled = false;
            yield return new WaitForSeconds(15);
            thrustEnabled = true;
            yield return null;
        }
        public void OnCollisionEnter(Collision other)
        {

            if (other.gameObject.tag == "Ball")
                isHit = false; 
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            
            if (ball == null)
            {
                ball = GameObject.FindGameObjectWithTag("Ball").transform;
                hitModifier = Random.Range(edgeSensitivity, 70f);
                isHit = true;
                StartCoroutine("noThrustOnStart");
            }
            else
            {   
                float ballX;
                float ballz;   
                    
                if (ball.position.y > 2 && !checkRand)
                    checkRand = true;
                
                if (ball.position.y < 0 && isHit)
                {  
                    if (checkRand)
                    {
                        hitModifier = Random.Range(edgeSensitivity, 70f);
                        checkRand = false;
                    }
                    if (hitModifier >60 && hitModifier <= 64)
                    {
                        if (hitModifier <= 62)
                            ballX = ball.position.x + lrEdge;
                        else
                            ballX = ball.position.x - lrEdge;
                        ballz = ball.position.z;
                    }
                    else if (hitModifier > 50 && hitModifier <= 54 )
                    {
                        if (hitModifier <= 52)
                            ballz = ball.position.z + udEdge;
                        else
                            ballz = ball.position.z - udEdge;
                        ballX = ball.position.x;
                    }
                    else
                    {
                        ballX = ball.position.x;
                        ballz = ball.position.z;
                    }
                    Vector3 pos = transform.position;
                    pos.x = Mathf.Clamp(ballX, negXBound, posXBound);
                    pos.z = Mathf.Clamp(ballz, negZBound, posZBound);
                    transform.position = transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
                    if ((hitModifier > 24f && hitModifier < 29f) && ball.position.y < -22f && thrustEnabled)
                        StartCoroutine("thrust");
                }
                if (ball.GetComponent<Ball>().speed.y < 0)
                    isHit = true;
                if (ball.GetComponent<Ball>().speed.y > 90f)
                    speed = 35f;
            }  
        }
    }
}
