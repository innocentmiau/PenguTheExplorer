using System.Collections;
using UnityEngine;

namespace Scripts.Map
{
    public class Cat : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform fakeCenterPointTrans;
        
        public Vector3 Collected()
        {
            StartCoroutine(FadeOutAnimation());
            return fakeCenterPointTrans.position;
        }

        private IEnumerator FadeOutAnimation()
        {
            float elapsed = 0f;
            Color color = spriteRenderer.color;
            while (true)
            {
                elapsed += Time.deltaTime;
                color.a = Mathf.Lerp(1f, 0f, elapsed);
                spriteRenderer.color = color;
                if (elapsed >= 1f) break;
                yield return null;
            }
            color.a = 0f;
            spriteRenderer.color = color;
            gameObject.SetActive(false);
        }
        
    }
}