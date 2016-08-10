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
        float hitModifier = 0f;
        bool checkRand = false;
        float lrEdge = 2.13f;
        float udEdge = 1.27f;
        float edgeSensitivity = 20f;
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
            {   //2.2f left and right edge
                //1.3f up and down edge
                float ballX;
                float ballz;   
                    
               if (ball.position.y > 2 && !checkRand)
                    checkRand = true;
                if (ball.position.y < 0)
                {  
                    if (checkRand)
                    {
                        hitModifier = Random.Range(edgeSensitivity, 70f);
                        checkRand = false;
                        Debug.Log("edgeModifier: " + hitModifier.ToString());
                    }
                    if (hitModifier >60 && hitModifier <= 70)
                    {
                        if (hitModifier > 60 && hitModifier <= 65)
                            ballX = ball.position.x + lrEdge;
                        else
                            ballX = ball.position.x - lrEdge;
                        ballz = ball.position.z;
                    }
                    else if (hitModifier > 50 && hitModifier <= 60 )
                    {
                        if (hitModifier > 50 && hitModifier <= 55)
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
