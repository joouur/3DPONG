using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Pong.Gameplay
{
    public class FadeInObjects : MonoBehaviour
    {

        public Material mat;
        private Rigidbody rb;
        private Collider col;

        private float alphaMat;
        public float fadeSpeed = 0.8f;
        private int fadeDir = 1;
        private float alpha;

        public bool hasRB;
        public void Awake()
        {
            //mat = gameObject.GetComponent<Material>();

            if (hasRB)
            {
                rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;

            }
            col = gameObject.GetComponent<Collider>();

            alphaMat = mat.color.a;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 0.0f);
            col.isTrigger = true;
            //StartCoroutine(fadeIn());
        }

        public float BeginFade(int direction)
        {
            fadeDir = direction;
            return (fadeSpeed);
        }

        private void Update()
        {
            if (Mathf.Round(alpha) != Mathf.Round(alphaMat))
                alpha += fadeDir * fadeSpeed * Time.deltaTime;
            else
            {
                alpha = alphaMat;
                if (hasRB)
                    rb.isKinematic = false;
                col.isTrigger = false;
            }
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
            //yield return new WaitForEndOfFrame();

        }
    }

}