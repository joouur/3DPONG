using UnityEngine;
using System.Collections;
using Pong.UI;

namespace Pong.Managers
{
    public class HelpSpawnManager : MonoBehaviour
    {
        public static HelpSpawnManager Instance;

        private GameObject RotObj;
        private GameObject RotTran;

        private GameObject RampObj;
        private GameObject RampTran;

        private GameObject StepObj;
        private GameObject StepTran;


        public Vector3 oldPosition;
        public Vector3 newPosition;

        public int randN;
        private bool[] objs;
        public void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Game Manager is already in play. Deleting old Instantiating new.");
                Destroy(gameObject);
            }
            else
                Instance = this;

            RotObj = Resources.Load("Prefabs/RotJoint", typeof(GameObject)) as GameObject;
            RampObj = Resources.Load("Prefabs/Ramp", typeof(GameObject)) as GameObject;
            StepObj = Resources.Load("Prefabs/Steps", typeof(GameObject)) as GameObject;

            randN = Random.Range(0, 4);
            objs = new bool[4];
            newPosition = setNewPosition();
        }

        public void SetNewObject()
        {
            if(ScoreUI.Instance.bounces % 5 == 0)
            {
                randN = Random.Range(0, 4);
                if (spawnedAlready(objs))
                {
                    objs = new bool[4];
                    randN = Random.Range(0, 4);
                }
                else 
                {
                    while(objs[randN] == true)
                    {
                        randN = Random.Range(0, 4);
                    }
                    objs[randN] = true;
                }

                switch (randN)
                {
                    case 0:
                        Debug.Log("Nothing Spawned");
                        break;
                    case 1:
                        RotObjNew();
                        Debug.Log("Rotator Spawned");
                        break;
                    case 2:
                        RampObjNew();
                        Debug.Log("Ramp Spawned");
                        break;
                    case 3:
                        StepsObjNew();
                        Debug.Log("Steps Spawned");
                        break;
                    default:
                        Debug.Log("Nothing Spawned");
                        break;
                }
            }
        }

        private Vector3 setNewPosition()
        {
            Vector3 nP = new Vector3((float)Random.Range(-5.0f, 6.0f), (float)Random.Range(-22.0f, 22.0f), (float)Random.Range(-3.5f, 4.5f));
            return nP;
        }

        #region Instantiating Objects
        private void RotObjNew()
        {
            if (!RotTran)
            {
                RotTran = Instantiate(RotObj, newPosition, new Quaternion(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180), 1)) as GameObject;
                oldPosition = newPosition;
                newPosition = setNewPosition();
            }
            else
                Debug.Log("There is a another RotationObject in play.");
        }

        public void RotReset()
        {
            Destroy(RotTran.gameObject);
            RotTran = null;
            //BallNew();
        }

        private void RampObjNew()
        {
            if (!RampTran)
            {
                RampTran = Instantiate(RampObj, newPosition, new Quaternion(Random.Range(0,180), Random.Range(0, 180), Random.Range(0, 180), 1)) as GameObject;
                oldPosition = newPosition;
                newPosition = setNewPosition();
            }
            else
                Debug.Log("There is a another RampObject in play.");
        }

        public void RampReset()
        {
            Destroy(RampTran.gameObject);
            RampTran = null;
            //BallNew();
        }

        private void StepsObjNew()
        {
            if (!StepTran)
            {
                StepTran = Instantiate(StepObj, newPosition, new Quaternion(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180), 1)) as GameObject;
                oldPosition = newPosition;
                newPosition = setNewPosition();
            }
            else
                Debug.Log("There is a another StepsObject in play.");
        }

        public void StepReset()
        {
            Destroy(StepTran.gameObject);
            StepTran = null;
            //BallNew();
        }
        #endregion

        public static bool spawnedAlready( bool[] a)
        {
            foreach (bool b in a)
            {
                if (!b)
                    return false;
            }
            return true;
        }
    }
}