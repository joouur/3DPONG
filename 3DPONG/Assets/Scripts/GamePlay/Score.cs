using UnityEngine;
using System.Collections;
using Pong.UI;
using Pong.Managers;

namespace Pong.Gameplay
{
    public class Score : MonoBehaviour
    {
        public bool plOrAI;

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ball")
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
}