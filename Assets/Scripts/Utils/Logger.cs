using UnityEngine;

namespace Scripts.Utils
{
    public class Logger : MonoBehaviour
    {

        [SerializeField] private bool enableLogs = true;

        public static Logger Instance;
        
        public static void Log(string message)
        {
            Instance.Message(message);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Message(string message)
        {
            if (enableLogs)
                Debug.Log(message);
        }
    }
}