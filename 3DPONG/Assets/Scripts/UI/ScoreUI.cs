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
        public Canvas WinnerScreen;

        private Text Winn;
        private Text PlayerScore;
        private Text AIScore;
        private Text Speed;
        private Text TimeS;
        private Text Bounces;
        private Text SongName;
        private Text ScoresComplete;

        public int bounces;

        public int pScore;
        public int aScore;
        private int fScore;

        private float playTime;

        private string C_Score;
        public int round;
        public int rA;
        public int rP;

        private static int delay = 10;

        // Use this for initialization
        public void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("ScoreUI is already in play. Deleting old Instantiating new.");
                Destroy(gameObject);
            }
            else
                Instance = this;

            pScore = 0;
            aScore = 0;
            fScore = 0;

            WinnerScreen.enabled = false;
            PlayerScore = GameObject.Find("PlayerScore").GetComponent<Text>();
            AIScore = GameObject.Find("AIScore").GetComponent<Text>();
            Speed = GameObject.Find("SpeedT").GetComponent<Text>();
            TimeS = GameObject.Find("TimeP").GetComponent<Text>();
            Bounces = GameObject.Find("BounceT").GetComponent<Text>();
            SongName = GameObject.Find("SongNameT").GetComponent<Text>();
            ScoresComplete = GameObject.Find("ScoresTXT").GetComponent<Text>();
            Winn = GameObject.Find("Winner").GetComponent<Text>();

            C_Score = "";
            ScoresComplete.text = C_Score;
            round = 0;
            rA = 0;
            rP = 0;
            playTime = 0.0f;
            bounces = 0;
            StartCoroutine(timeSet());


        }

        /*
        void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                aScore++;
                ScoreAI();

                BestOf();
            }
            else if(Input.GetKeyDown(KeyCode.L))
            {
                pScore++;
                ScorePlayer();
                BestOf();
            }
            else if( Input.GetKeyDown(KeyCode.M))
            {
                SetWinner(true);
            }
        }
        */

        public void ScorePlayer()
        {
            PlayerScore.text = pScore.ToString();
            ResetBounces();
        }
        public void ScoreAI()
        {
            AIScore.text = aScore.ToString();
            ResetBounces();
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

        private void ResetBounces()
        {
            bounces = 0;
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

        private bool fs;

        public void BestOf()
        {
            fScore++;
            if (fScore == 22)
            {
                if(aScore == pScore)
                {
                    fScore = 20;
                }
                else
                {
                    if (aScore >= 11)
                    {
                        C_Score = C_Score + string.Format("Winner AI {0} - {1} P\n", aScore, pScore);
                        rA = rA + 1;
                    }
                    else if (pScore >= 11)
                    {
                        C_Score = C_Score + string.Format("Winner P {0} - {1} AI\n", pScore, aScore);
                        rP = rP + 1;
                    }

                    if (round >= 4)
                    {
                        if (rA > 2)
                        {
                            fs = true;
                        }
                        else if (rP > 2)
                        {
                            fs = false;
                        }
                        SetWinner(fs);
                        //ResetScores();
                    }
                    else
                    {
                        Debug.Log(C_Score);
                        ScoresComplete.text = C_Score;

                        round++;
                        aScore = 0;
                        pScore = 0;
                        fScore = 0;
                        ScorePlayer();
                        ScoreAI();
                    }
                }
            }
        }

        private string sp;

        public void SetWinner(bool w)
        {
            EnableWinner();

            if (w)
                sp = "Artificial Intelligence!";
            else if (!w)
                sp = "PLAYER! (Impressive right?)";

            Winn.text = sp;
            Time.timeScale = 0;
            Cursor.visible = true;
        }

        private void EnableWinner()
        {
            WinnerScreen.enabled = !WinnerScreen.enabled;
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void ResetScores()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            Cursor.visible = !Cursor.visible;

            EnableWinner();
            aScore = pScore = fScore = 0;
            C_Score = "";
            ScoresComplete.text = C_Score;
            ScorePlayer();
            ScoreAI();
            round = 0;
            rA = 0;
            rP = 0;
            playTime = 0.0f;
            GameManager.Instance.BallReset();
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