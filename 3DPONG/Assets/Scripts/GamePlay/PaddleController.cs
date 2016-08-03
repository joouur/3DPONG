using UnityEngine;
using System.Collections;

namespace Pong.Gameplay
{
    public class PaddleController : MonoBehaviour
    {

        public float speed = 40f;
        float translationX; // movement of paddle in x direction
        float translationY; // movement of paddle in y direction
        public float translationThrust = 10f;
        public float paddleHome = 24.01f;
        public float negXBound = -3.96f;
        public float posXBound = 3.993f;
        public float negZBound = -3.21f;
        public float posZBound = 3.21f;
        public float smoothFactor = 0.3f;
        private Vector3 velocity = Vector3.zero;
        private Rigidbody pdRb;
        private Vector3 startPause;
        public float thrustSpeed = 1.2f;
        public bool thrustEnabled = true;
        public bool tilt = true;
        Vector3 startPos = new Vector3(-3.75f, 23.98f, 2.8f);
        //public bool thrustKey = Input.GetMouseButtonDown(0);
        // Use this for initialization
        void Start()
        {
            //Cursor.visible = false;
           
            transform.position = startPos;
            pdRb = GetComponent<Rigidbody>();
            GetComponent<Rigidbody>().isKinematic = true;
            //gameObject.GetComponent<Renderer>().material.color = 0;
            startPause = transform.position;
        }

        IEnumerator thrust()
        {
            Vector3 pos = new Vector3(0f, -40f, 0f);
            Vector3 backPos = new Vector3(transform.position.x, startPos.y, transform.position.z);
            
            if (thrustEnabled)
            {
                pdRb.MovePosition(transform.position + pos * Time.deltaTime);
                thrustEnabled = false;
            }
               
            yield return new WaitForSeconds(1);
            pdRb.MovePosition(backPos);
            thrustEnabled = true;
        }

        IEnumerator forwardTilt()
        {
            if (tilt)
            {
                transform.Rotate(25f, 0f, 0f);
                tilt = false;
            }
            yield return new WaitForSeconds(1);
            transform.Rotate(-25f, 0f, 0f);
            tilt = true; 
        }
        IEnumerator backwardTilt()
        {
            if (tilt)
            {
                transform.Rotate(-25f, 0f, 0f);
                tilt = false;
            }
            yield return new WaitForSeconds(1);
            transform.Rotate(25f, 0f, 0f);
            tilt = true;
        }
        IEnumerator leftTilt()
        {
            if (tilt)
            {
                transform.Rotate(0f, 0f, 25f);
                tilt = false;
            }
            yield return new WaitForSeconds(1);
            transform.Rotate(0f, 0f, -25f);
            tilt = true;
        }
        IEnumerator rightTilt()
        {
            if (tilt)
            {
                transform.Rotate(0f, 0f, -25f);
                tilt = false;
            }
            yield return new WaitForSeconds(1);
            transform.Rotate(0f, 0f, 25f);
            tilt = true;
        }

        // Update is called once per frame
        void Update()
        {
            //these two translations get the mouse position and multiply it by a set speed
            //it is then multiplied by timedelta and then the translation is made.
            Vector3 pos = transform.position;
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            translationX = mouseX * speed;
            translationY = mouseY * speed;
            translationX *= Time.deltaTime;
            translationY *= Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {     
                StartCoroutine("thrust");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine("forwardTilt");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine("backwardTilt");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine("leftTilt");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine("rightTilt");
            }


            pos.x = Mathf.Clamp(pos.x + translationX, negXBound, posXBound);
            pos.z = Mathf.Clamp(pos.z + translationY, negZBound, posZBound);
            //transform.position = Vector3.SmoothDamp(transform.position, pos, ref speed, smoothFactor);
            //transform.position = pos;
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
        }
    }
}
