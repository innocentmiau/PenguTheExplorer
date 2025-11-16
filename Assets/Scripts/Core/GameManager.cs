using System.Collections.Generic;
using UnityEngine;
using Logger = Scripts.Utils.Logger;

namespace Scripts.Core
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] private AudioClip mainMusic;
        [SerializeField] private AudioClip gameMusic;
        [SerializeField] private float fadeIn = 5f;
        [SerializeField] private float fadeOut = 5f;
        
        public static GameManager Instance;
        
        private AudioSource _audioSource;
        private AudioClip _musicPlaying;

        private float _maxVolume;
        private float _fadeInElapse;
        private float _fadeOutElapse;
        
        public bool[] CatsCollected { get; private set; }

        public void SetCatCollected(int numb) => CatsCollected[numb] = true;
        public bool IsCatCollected(int numb) => CatsCollected[numb];
        
        private void Start()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            CatsCollected = new bool[10];
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
            _musicPlaying = mainMusic;
            _maxVolume = _audioSource.volume;
        }

        public void ChangeToLevelScene()
        {
            _musicPlaying = gameMusic;
            _fadeOutElapse = fadeOut;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        }

        public void ChangeToMainScene()
        {
            _musicPlaying = mainMusic;
            _fadeOutElapse = fadeOut;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }

        private void Update()
        {
            if (_fadeInElapse > 0)
            {
                _fadeInElapse -= Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(_maxVolume, 0f, _fadeInElapse / fadeIn);
            }

            if (_fadeOutElapse > 0)
            {
                _fadeOutElapse -= Time.deltaTime;
                _audioSource.volume = Mathf.Lerp(0f, _maxVolume, _fadeOutElapse / fadeOut);
            }

            if (_audioSource.isPlaying && _fadeOutElapse <= 0f)
            {
                float timeTillFinish = _audioSource.clip.length - _audioSource.time;
                if (timeTillFinish <= fadeOut)
                    _fadeOutElapse = fadeOut;
            }

            if (!_audioSource.isPlaying)
            {
                _audioSource.clip = _musicPlaying;
                _audioSource.volume = 0f;
                _fadeInElapse = fadeIn;
                _audioSource.Play();
            }
        }
        
    }
}