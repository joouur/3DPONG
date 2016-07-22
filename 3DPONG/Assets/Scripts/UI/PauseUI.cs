using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Pong.UI
{
    public class PauseUI : MonoBehaviour
    {
        public static PauseUI Instance;

        public Canvas PauseMenu;
        public Canvas MainMenu;
        public Canvas OptionsMenu;
        private bool paused;

        void Start()
        {
            if (Instance != null)
            {
                Debug.Log("PauseUI is already in play. Deleting old Instantiating new.");
                Destroy(gameObject);
            }
            else
                Instance = this;

            PauseMenu.enabled = false;
            MainMenu.enabled = false;
            OptionsMenu.enabled = false;
            //OptionsMenu.enabled = false;
            paused = false;
        }

        // Update is called once per frame
        public void LateUpdate()
        {
            if(Time.timeScale == 1)
            {
                FalseIt();
            }
            if (Input.GetButtonDown("Cancel"))
            {
                Pause();
            }

        }

        private void Pause()
        {
            PauseMenu.enabled = !PauseMenu.enabled;
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            MainMenu.enabled = !MainMenu.enabled;
            //AudioListener.pause = !AudioListener.pause;
        }

        private void FalseIt()
        {
            MainMenu.enabled = false;
            OptionsMenu.enabled = false;
        }

        public void Options()
        {
            OptionsMenu.enabled = !OptionsMenu.enabled;
            MainMenu.enabled = !MainMenu.enabled;
        }
    }
}