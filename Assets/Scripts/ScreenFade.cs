using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ScreenFade : MonoBehaviour
    {
        public Texture2D fadeOutTexture;
        public float fadeSpeed = 0.8f;

        private int drawDepth = -1000; // the order that the texture will be drawn in
        private float alpha = 1.0f;
        private int fadeDir = -1;

        void OnGUI()
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha); // Set gui colors to the same except for alpha
            GUI.depth = drawDepth;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
        }

        public float BeginFade(int direction)
        {
            fadeDir = direction;
            return fadeSpeed;
        }

        void OnLevelWasLoaded()
        {
            BeginFade(-1);
        }

    }
}
