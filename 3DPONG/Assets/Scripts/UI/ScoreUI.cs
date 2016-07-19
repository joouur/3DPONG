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
        private Text TimeS;
        private Text Bounces;

        public int bounces;
        public int pScore;
        public int aScore;
        private float playTime;

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
            TimeS = GameObject.Find("TimeP").GetComponent<Text>();
            Bounces = GameObject.Find("BounceT").GetComponent<Text>();
            playTime = 0.0f;
            bounces = 0;
            StartCoroutine(timeSet());
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
        
        public void SetBounces()
        {
            bounces++;
            Bounces.text = bounces.ToString();
        }

        private IEnumerator timeSet()
        {
            while (true)
            {
                TimeS.text = Mathf.RoundToInt(playTime).ToString() + " s";
                yield return new WaitForEndOfFrame();
                playTime += Time.deltaTime;
                //Debug.Log(playTime);
            }
        }
    }
}