using System;
using Scripts.Map;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {

        public event Action<Vector3> OnCollectCat; 
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Collectable"))
            {
                if (other.TryGetComponent(out Cat cat))
                    OnCollectCat?.Invoke(cat.Collected());
            }
                
        }
    }
}