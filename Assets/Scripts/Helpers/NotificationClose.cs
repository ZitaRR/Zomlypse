using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zomlypse.Behaviours;
using Zomlypse.Singleton;

namespace Zomlypse.Helpers
{
    public class NotificationClose : MonoBehaviour, IPointerClickHandler
    {
        private TextMeshProUGUI text;
        private TextLinker linker;
        private Notifications notifications;
        private CharacterWindow charWindow;

        private void Start()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            linker = GameManager.Instance.Linker;
            notifications = GameManager.Instance.Notifications;
            charWindow = GameManager.Instance.CharacterWindow;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Entity entity = linker.GetLinkCharacter(text);
            if (entity != null)
            {
                charWindow.Enable(entity);
                return;
            }

            GameManager.Instance.Notifications.DeleteNotification(transform.GetSiblingIndex());
        }
    }
}
