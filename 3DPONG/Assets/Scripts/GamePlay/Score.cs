using UnityEngine;
using System.Collections;
using Pong.UI;

namespace Pong.UI
{
    public class Score : MonoBehaviour
    {
        public bool plOrAI;


        public void OnTriggerEnter()
        {
            if (plOrAI)
            {
                ScoreUI.Instance.pScore++;
                ScoreUI.Instance.ScorePlayer();
            }
            else
            {
                ScoreUI.Instance.aScore++;
                ScoreUI.Instance.ScoreAI();
            }
            GameManager.Instance.BallReset();
        }
    }
}