using UnityEngine;
using UnityEngine.EventSystems;
using Zomlypse.Behaviours;

namespace Zomlypse.Helpers
{
    public class PromptClose : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private Transform root;
        [SerializeField]
        private bool value;

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.Instance.Notifications.DeletePrompt(root.GetSiblingIndex(), value);
        }
    }
}
