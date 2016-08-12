using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Pong.UI
{
    public class FadeInScene : MonoBehaviour
    {
        public static FadeInScene Instance;

        public Texture2D fadeOutTexture;
        public float fadeSpeed = 0.8f;

        private int drawDepth = -1000;
        private float alpha = 1.0f;

        private int fadeDir = -1;
        
        void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("FadeInScene is already in play. Deleting old Instantiating new.");
                Destroy(gameObject);
            }
            else
                Instance = this;
        }
        
        private void OnGUI()
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.depth = drawDepth;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
        }

        public float BeginFade(int direction)
        {
            fadeDir = direction;
            return (fadeSpeed);
        }

        void OnLevelWasLoaded()
        {
            BeginFade(-1);
        }

        public IEnumerator onChangeLevel(int n)
        {
            float fadeTime = BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene(n);
        }
    }
}