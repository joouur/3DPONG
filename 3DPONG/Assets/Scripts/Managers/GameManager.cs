using UnityEngine;
using System.Collections;
using Pong.UI;

namespace Pong.Managers
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

        private GameObject RBObj;
        private GameObject RBTran;


        public void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Game Manager is already in play. Deleting old Instantiating new.");
                Destroy(gameObject);
            }
            else
                Instance = this;

            RBObj = Resources.Load("Prefabs/RollerBall", typeof(GameObject)) as GameObject;
            BallNew();
        }

        public void BallReset()
        {
            Destroy(RBTran.gameObject);
            RBTran = null;
            BallNew();
        }

        public void ResetTheBall()
        {
            BallReset();
            if (ScoreUI.Instance.pScore != 0)
            {
                ScoreUI.Instance.pScore--;
                ScoreUI.Instance.ScorePlayer();
            }
        }

        public void BallNew()
        {
            if (!RBTran)
                RBTran = Instantiate(RBObj, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            else
                Debug.Log("There is a main Ball in play.");
        }
    }
}