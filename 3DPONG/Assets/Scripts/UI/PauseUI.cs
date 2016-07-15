using UnityEngine;
using System.Collections;

namespace pong.UI
{
    public class PauseUI : MonoBehaviour
    {

        public Canvas PauseMenu;
        public Canvas MainMenu;
        public Canvas OptionsMenu;
        private bool paused;
        void Start()
        {

            PauseMenu.enabled = false;
            MainMenu.enabled = false;
            OptionsMenu.enabled = false;
            //OptionsMenu.enabled = false;
            paused = true;
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

        public void Pause()
        {
            PauseMenu.enabled = !PauseMenu.enabled;
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            MainMenu.enabled = !MainMenu.enabled;
            
        }

        public void Options()
        {
            OptionsMenu.enabled = !OptionsMenu.enabled;
            MainMenu.enabled = !MainMenu.enabled;
        }

        private void FalseIt()
        {
            MainMenu.enabled = false;
            OptionsMenu.enabled = false;
        }
    }
}