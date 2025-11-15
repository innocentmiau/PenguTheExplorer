using System.Collections;
using Scripts.Player;
using TMPro;
using UnityEngine;

namespace Scripts.Core
{
    public class SceneManager : MonoBehaviour
    {
        
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject popupText;

        private void Awake()
        {
            playerController.OnCollectCat += PlayerCollectedCat;
        }

        private void PlayerCollectedCat(Vector3 pos)
        {
            StartCoroutine(HandleText(Instantiate(popupText, pos, Quaternion.identity)));
        }

        private IEnumerator HandleText(GameObject obj)
        {
            TMP_Text tmp = obj.GetComponent<TMP_Text>();
            Color tmpColor = tmp.color;
            float elapsed = 0f;
            while (elapsed < 1f)
            {
                elapsed += Time.fixedDeltaTime;
                tmpColor.a = Mathf.Lerp(1f, 0f, elapsed / 1f);
                tmp.color = tmpColor;
                yield return null;
            }
            Destroy(obj);
        }
    }
}