using UnityEngine;
using System.Collections;
using Pong.UI;
using Pong.Managers;

namespace Pong.Gameplay
{
    public class Ball : MonoBehaviour
    {

        public Vector3 speed;
        public float startSpeed;
        [HideInInspector]
        public Rigidbody BallRb;

        public float magnitude;
        private Vector3 grav;
        public float maxAngularSpeed;

        //Audio
        private AudioSource ballAudio;

        private PaddleContact playerContact;
        private PaddleContact enemyContact;

        public void Awake()
        {
            BallRb = GetComponent<Rigidbody>();
            //BallRb.useGravity = false;
            //BallRb.AddRelativeForce(Vector3.one * 500);
            if (startSpeed == 0)
                startSpeed = -15.0f;
            BallRb.maxAngularVelocity = maxAngularSpeed;
            BallRb.AddRelativeTorque(Vector3.one);
            BallRb.velocity = new Vector3(0, startSpeed, 0);
            grav = new Vector3(0, -9.81f, 0);

            playerContact = GameObject.FindGameObjectWithTag("Player").GetComponent<PaddleContact>();
            enemyContact = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PaddleContact>();

            playerContact.ball = this;
            enemyContact.ball = this;

            ballAudio = GetComponent<AudioSource>() as AudioSource;
        }
        // Update is called once per frame
        public void FixedUpdate()
        {
            magnitude = BallRb.velocity.magnitude;
            ScoreUI.Instance.SetSpeed(magnitude);
        }


        public void OnCollisionEnter(Collision other)
        {
            if (other.collider.tag == "Player" || other.collider.tag == "Enemy")
            {
                if (other.collider.tag == "Player")
                {
                    ScoreUI.Instance.SetBounces();
                    ColSound(0, AudioManager.Instance.soundVolume);
                }
                else
                {
                    ColSound(1, (int)(AudioManager.Instance.soundVolume * 0.75f));
                }
                Physics.gravity = grav;
                grav = grav * -1.0f;
                speed = BallRb.velocity;
                if (!(BallRb.velocity.magnitude > 100))
                {
                    speed *= 1.025f;
                    //Debug.Log(BallRb.velocity.magnitude);
                    BallRb.velocity = speed;

                }
            }
            else
            {
                ColSound(2, (int)(AudioManager.Instance.soundVolume * 0.5f));
            }
        }

        public void ColSound(int i, int v)
        {
            ballAudio.PlayOneShot(AudioManager.ballAudioClips.ballCollision[i], v / 100f);
        }
    }
}