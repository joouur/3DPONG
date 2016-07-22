using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Pong.Managers;

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
        private Text SongName;
        public int bounces;
        public int pScore;
        public int aScore;
        private float playTime;

        private static int delay = 10;
        // Use this for initialization
        public void Start()
        {
            pScore = 0;
            aScore = 0;
            
            PlayerScore = GameObject.Find("PlayerScore").GetComponent<Text>();
            AIScore = GameObject.Find("AIScore").GetComponent<Text>();
            Speed = GameObject.Find("SpeedT").GetComponent<Text>();
            TimeS = GameObject.Find("TimeP").GetComponent<Text>();
            Bounces = GameObject.Find("BounceT").GetComponent<Text>();
            SongName = GameObject.Find("SongNameT").GetComponent<Text>();
            playTime = 0.0f;
            bounces = 0;
            StartCoroutine(timeSet());
        }

        public void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("ScoreUI is already in play. Deleting old Instantiating new.");
                Destroy(gameObject);
            }
            else
                Instance = this;
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

        private string s;

        public IEnumerator SongNames()
        {
            int i = 0;
            //int j = 10;
            while (true)
            {
                if (s != AudioManager.Instance.GetSongName())
                {
                    s = AudioManager.Instance.GetSongName();
                    i = 0;
                }
                if (s.Length <= 20)
                {
                    SongName.text = s;
                    SongName.resizeTextForBestFit = true;
                }
                else
                {
                    SongName.resizeTextForBestFit = false;
                    SongName.text = s.Substring(i, 14).ToString();
                    //Debug.Log(string.Format("in i, i = {0}, length = {1}", i, s.Length));
                    if (i == s.Length - 13)
                        i = 0;
                }
                i++;
                if (i == s.Length - 13)
                {
                    i = 0;
                }
                yield return new WaitForSeconds(Time.deltaTime * delay);
            }
        }
    }
}