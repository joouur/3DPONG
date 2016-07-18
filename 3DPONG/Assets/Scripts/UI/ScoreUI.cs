using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pong.UI;

namespace Pong.UI
{
    public class ScoreUI : MonoBehaviour
    {
        public static ScoreUI Instance;

        public Canvas PlayUI;
        private Text PlayerScore;
        private Text AIScore;
        private Text Speed;
        public int pScore;
        public int aScore;

        // Use this for initialization
        public void Start()
        {
            pScore = 0;
            aScore = 0;
            if (Instance != null)
            {
                Debug.Log("PauseUI is already in play. Deleting old Instantiating new.");
                Destroy(gameObject);
            }
            else
                Instance = this;
            PlayerScore = GameObject.Find("PlayerScore").GetComponent<Text>();
            AIScore = GameObject.Find("AIScore").GetComponent<Text>();
            Speed = GameObject.Find("SpeedT").GetComponent<Text>();
        }

        public void ScorePlayer()
        {
            PlayerScore.text = pScore.ToString();
        }
        public void ScoreAI()
        {
            AIScore.text = aScore.ToString();
        }
        public void SetSpeed(float s)
        {
            Speed.text = s.ToString("0.0") + "m/s";
        }
    }
}