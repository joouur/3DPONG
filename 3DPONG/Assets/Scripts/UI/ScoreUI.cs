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
        private Text SongName;
        public int bounces;
        public int pScore;
        public int aScore;
        private float playTime;

        private static int delay = 1;
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

        public IEnumerator SongNames()
        {
            int i = 0;
            int j = 13;
            while (true)
            {
                string s = AudioManager.Instance.GetSongName();
                string temp = s;
                SongName.text = temp.Substring(i, j).ToString();
                i++;
                j++;
                Debug.Log(string.Format("i = {0}, j = {1}", i, j));
                if (j >= temp.Length)
                {
                    j = s.Length - j;
                    Debug.Log(string.Format("in j, i = {0}, j = {1}", i, j));

                }
                if (i >= temp.Length)
                {
                    i = 0;
                    Debug.Log(string.Format("in i, i = {0}, j = {1}", i, j));

                }
                yield return new WaitForSeconds(delay);
            }
        }
    }
}