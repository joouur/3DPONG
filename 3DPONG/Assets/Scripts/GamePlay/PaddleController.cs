﻿using UnityEngine;
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
        public float thrustSpeed = 10f;
        public bool thrustEnabled = true;
        public bool ftilt = true;
        public bool btilt = true;
        public bool ltilt = true;
        public bool rtilt = true;
        Vector3 startPos = new Vector3(-3.75f, 23.98f, 2.8f);
        //public bool thrustKey = Input.GetMouseButtonDown(0);
        // Use this for initialization
        void Start()
        {
            //Cursor.visible = false;
           
            transform.position = startPos;
            pdRb = GetComponent<Rigidbody>();
            pdRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            GetComponent<Rigidbody>().isKinematic = true;
            //gameObject.GetComponent<Renderer>().material.color = 0;
            startPause = transform.position;
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
            //yield return new WaitForSeconds(0.1f);
            //pdRb.MovePosition(new Vector3(transform.position.x, startPos.y, transform.position.z));
            //yield return new WaitForSeconds(0.018f);
            //thrustEnabled = true;  
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
            //yield return new WaitForSeconds(0.12f);
            //transform.rotation = Quaternion.identity;
            //yield return new WaitForSeconds(0.018f);
            //ftilt = true; 
        }
        IEnumerator backwardTilt()
        {
            if (btilt)
            {
                transform.Rotate(-25f, 0f, 0f);  
                btilt = false;
            }
            yield return new WaitForEndOfFrame();
            // yield return new WaitForSeconds(0.12f);
            //transform.rotation = Quaternion.identity;
            //yield return new WaitForSeconds(0.018f);
            // btilt = true;
        }
        IEnumerator leftTilt()
        {
            if (ltilt)
            {
                transform.Rotate(0f, 0f, 25f);  
                ltilt = false;
            }
            yield return new WaitForEndOfFrame();
            //yield return new WaitForSeconds(0.12f);
            //transform.rotation = Quaternion.identity;
            //yield return new WaitForSeconds(0.018f);
            //ltilt = true;
        }
        IEnumerator rightTilt()
        {
            if (rtilt)
            {
                transform.Rotate(0f, 0f, -25f);  
                rtilt = false;
            }
            yield return new WaitForEndOfFrame();
            //yield return new WaitForSeconds(0.12f);
            //transform.rotation = Quaternion.identity;
            //yield return new WaitForSeconds(0.018f);
            //rtilt = true;
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
            //transform.position = Vector3.SmoothDamp(transform.position, pos, ref speed, smoothFactor);
            //transform.position = pos;
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * speed);
        }
    }
}
