using UnityEngine;

namespace Scripts.Player
{
    public class RotateFromMain : MonoBehaviour
    {

        public void UpdateRotation(Transform trans)
        {
            transform.localRotation = trans.rotation;
        }
        
    }
}