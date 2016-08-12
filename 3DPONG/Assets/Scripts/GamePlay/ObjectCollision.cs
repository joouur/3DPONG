using UnityEngine;
using System.Collections;
using Pong.UI;
using Pong.Managers;

namespace Pong.Gameplay
{
    public class ObjectCollision : MonoBehaviour
    {

        public enum TypeOBJ
        {
            Ramp,
            Rotator,
            Steps
        }

        public TypeOBJ typeOb;

        private int Bounce;
        private float delayed;
        private bool waitforit;


        void Awake()
        {
            Bounce = 4;
            delayed = 10.0f;
            waitforit = true;
            StartCoroutine(StartPlaying());
        }

        IEnumerator StartPlaying()
        {
            while (true)
            {
                if (Bounce <= 0 || delayed <= 0.1f)
                {
                    if (typeOb == TypeOBJ.Ramp)
                    {
                        HelpSpawnManager.Instance.RampReset();
                    }
                    else if (typeOb == TypeOBJ.Rotator)
                    {
                        HelpSpawnManager.Instance.RotReset();
                    }
                    else if (typeOb == TypeOBJ.Steps)
                    {
                        HelpSpawnManager.Instance.StepReset();
                    }
                }
                else
                {
                    delayed -= Time.deltaTime;
                }
                //Quaternion newRot = new Quaternion(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2), 1.0f);
               
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }


        public void OnCollisionEnter(Collision other)
        {
            if(other.collider.tag == "Ball" && waitforit)
            {
                Bounce -= 1;
                waitforit = false;
                Invoke("GetTrueForIt", 1.0f);
            }
        }

        private void GetTrueForIt()
        {
            waitforit = true;
        }

    }
}