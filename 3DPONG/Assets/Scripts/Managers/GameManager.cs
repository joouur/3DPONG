using UnityEngine;
using System.Collections;
using Pong.UI;
using Pong.Gameplay;

namespace Pong.Managers
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

        private GameObject RBObj;
        private GameObject RBTran;
        private GameObject aiPaddle;


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
        public void ResetAI()
        {
            aiPaddle = GameObject.FindGameObjectWithTag("Enemy");
            aiPaddle.GetComponent<AiController>().isHit = true;
        }

        public void ResetTheBall()
        {
            BallReset();
            ResetAI();
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