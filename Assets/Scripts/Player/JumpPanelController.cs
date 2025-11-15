using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Player
{
    public class JumpPanelController : MonoBehaviour
    {

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image fill1;
        [SerializeField] private Image fill2;

        private void Start()
        {
            ResetBar();
        }

        public void ResetBar()
        {
            canvasGroup.alpha = 0f;
            fill1.fillAmount = 0f;
            fill2.fillAmount = 0f;
        }
    
        public void UpdateBars(float perc)
        {
            fill1.fillAmount = Mathf.Min(1f, (perc + 0.05f));
            fill2.fillAmount = Mathf.Min(1f, perc);
            canvasGroup.alpha = Mathf.Min(1f, perc);
        }
    }

}