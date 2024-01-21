using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Behaviours
{
    public class Frame : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private RectTransform rect;

        private bool dragging = false;
        private Vector2 offset;

        private void Update()
        {
            if (!dragging)
            {
                return;
            }

            rect.transform.position = (Vector2)Input.mousePosition - offset;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            dragging = true;
            offset = eventData.position -(Vector2)transform.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            dragging = false;
        }
    }
}
