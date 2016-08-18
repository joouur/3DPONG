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
        private Rigidbody pdRb;
        public float thrustSpeed = 10f;
        public bool thrustEnabled = true;
        public bool ftilt = true;
        public bool btilt = true;
        public bool ltilt = true;
        public bool rtilt = true;
        Vector3 startPos = new Vector3(-3.75f, 23.98f, 2.8f);
        // Use this for initialization
        void Start()
        {
            transform.position = startPos;
            pdRb = GetComponent<Rigidbody>();
            pdRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        IEnumerator thrust()
        {
            if (thrustEnabled)
            {
                Vector3 pos = new Vector3(0f, -40f, 0f);
                pdRb.MovePosition(transform.position + pos * Time.deltaTime);
                thrustEnabled = false;
            }
            yield return new WaitForEndOfFrame();
        }
        IEnumerator thrustReturn()
        {
            pdRb.MovePosition(new Vector3(transform.position.x, startPos.y, transform.position.z));
            thrustEnabled = true;
            yield return new WaitForEndOfFrame();
        }
        IEnumerator forwardTilt()
        {
            if (ftilt)
            {
                transform.Rotate(25f, 0f, 0f);      
                ftilt = false;
            }
            yield return new WaitForEndOfFrame();
        }
        IEnumerator backwardTilt()
        {
            if (btilt)
            {
                transform.Rotate(-25f, 0f, 0f);  
                btilt = false;
            }
            yield return new WaitForEndOfFrame();
        }
        IEnumerator leftTilt()
        {
            if (ltilt)
            {
                transform.Rotate(0f, 0f, 25f);  
                ltilt = false;
            }
            yield return new WaitForEndOfFrame();
        }
        IEnumerator rightTilt()
        {
            if (rtilt)
            {
                transform.Rotate(0f, 0f, -25f);  
                rtilt = false;
            }
            yield return new WaitForEndOfFrame();
        }
        IEnumerator rightReturn()
        {
            transform.rotation = Quaternion.identity;
            rtilt = true;
            yield return new WaitForEndOfFrame();

        }
        IEnumerator leftReturn()
        {
            transform.rotation = Quaternion.identity;
            ltilt = true;
            yield return new WaitForEndOfFrame();

        }
        IEnumerator backwardReturn()
        {
            transform.rotation = Quaternion.identity;
            btilt = true;
            yield return new WaitForEndOfFrame();

        }
        IEnumerator forwardReturn()
        {
            transform.rotation = Quaternion.identity;
            ftilt = true;
            yield return new WaitForEndOfFrame();

        }
            
        // Update is called once per frame
        void Update()
        {
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
                Debug.Log("MouseClicked");
            }
            if (Input.GetMouseButtonUp(0))
                StartCoroutine("thrustReturn");
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine("forwardTilt");
                Debug.Log("UpClicked");
            }
            if (Input.GetKeyUp(KeyCode.W))
                StartCoroutine("forwardReturn");

            if (Input.GetKeyDown(KeyCode.S))
            {
                StartCoroutine("backwardTilt");
                Debug.Log("DownClicked");
            }
            if (Input.GetKeyUp(KeyCode.S))
                StartCoroutine("backwardReturn");
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine("leftTilt");
                Debug.Log("LeftClicked");
            }
            if (Input.GetKeyUp(KeyCode.A))
                StartCoroutine("leftReturn");
            if (Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine("rightTilt");
                Debug.Log("RightClicked");
            }
            if (Input.GetKeyUp(KeyCode.D))
                StartCoroutine("rightReturn");
            pos.x = Mathf.Clamp(pos.x + translationX, negXBound, posXBound);
            pos.z = Mathf.Clamp(pos.z + translationY, negZBound, posZBound);
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
        }
    }
}
