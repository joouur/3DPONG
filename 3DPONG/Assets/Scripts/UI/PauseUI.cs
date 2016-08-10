using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Pong.UI
{
    public class PauseUI : MonoBehaviour
    {
        public static PauseUI Instance;

        public Canvas PauseMenu;
        public Canvas MainMenu;
        public Canvas OptionsMenu;


        void Awake()
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

            Cursor.visible = !Cursor.visible;
            //OptionsMenu.enabled = false;
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
            Cursor.visible = !Cursor.visible;

            MainMenu.enabled = !MainMenu.enabled;
            //AudioListener.pause = !AudioListener.pause;
        }
        
        public void MenuScene()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitMenu()
        {
            Application.Quit();
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