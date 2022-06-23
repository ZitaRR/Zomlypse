using UnityEngine;
using UnityEngine.EventSystems;

namespace Zomlypse.Helpers
{
    public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private LeanTweenType easing;
        [SerializeField]
        private float offset;
        [SerializeField]
        private float duration;

        private RectTransform rect;
        private Vector2 origin;
        private Vector2 target;
        private LTDescr ltd;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!UI.IsTweening(rect, out _))
            {
                origin = rect.anchoredPosition;
                target = new Vector2(origin.x, origin.y - offset);
            }

            ltd = UI.Move(rect, target, duration)
                .setEase(easing);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (ltd == null)
            {
                return;
            }

            LeanTween.cancel(ltd.rectTransform);
            ltd = UI.Move(rect, origin, duration, () => ltd = null)
                .setEase(easing);
        }
    }
}
