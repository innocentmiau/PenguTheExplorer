namespace Scripts.Utils
{
    using UnityEngine;

    public class FPSCounter : MonoBehaviour
    {
        private float fps;
        private float updateInterval = 0.5f;
        private float accum = 0f;
        private int frames = 0;
        private float timeLeft;
    
        void Start()
        {
            timeLeft = updateInterval;
        }
    
        void Update()
        {
            timeLeft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            frames++;
        
            if (timeLeft <= 0f)
            {
                fps = accum / frames;
                timeLeft = updateInterval;
                accum = 0f;
                frames = 0;
            }
        }
    
        void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 20), $"FPS: {fps:F0}");
        }
    }
}