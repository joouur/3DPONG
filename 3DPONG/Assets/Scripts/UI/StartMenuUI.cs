using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Pong.Managers;

namespace Pong.UI
{
    public class StartMenuUI : MonoBehaviour
    {
        public static StartMenuUI Instance;

        private Animator anim;
        public Canvas Menu;
        public Canvas OptionsMenu;

        public GameObject panelStart;
        public GameObject panelEnd;

        private Text SongName;

        void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("PauseUI is already in play. Deleting old Instantiating new.");
                Destroy(gameObject);
            }
            else
                Instance = this;
            OptionsMenu.enabled = false;

            Cursor.visible = true;
            if(!anim)
            {
                anim = panelStart.GetComponent<Animator>();
            }
            //panelStart.SetActive(true);
            panelEnd.SetActive(true);
            //anim.SetBool("Start", false);
            //StartCoroutine(StartScreen());
            StartCoroutine(SongNames());
            SongName = GameObject.Find("SongName").GetComponent<Text>();

        }

        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void Options()
        {
            OptionsMenu.enabled = !OptionsMenu.enabled;
            Menu.enabled = !Menu.enabled;
        }
        
        IEnumerator StartScreen()
        {

            yield return new WaitForSeconds(0.5f);

            anim.SetBool("Start", true);
            yield return new WaitForSeconds(2.0f);
            panelEnd.SetActive(true);
            panelStart.SetActive(false);
        }

        public void ExitGame()
        {
            Application.Quit();
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
                yield return new WaitForSeconds(Time.deltaTime * 10);
            }
        }
    }
}