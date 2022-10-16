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

        private void Start()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            linker = GameManager.Instance.Linker;
            notifications = GameManager.Instance.Notifications;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Entity entity = linker.GetLinkCharacter(text);
            if (entity != null)
            {
                notifications.Add(new Notification(
                    "Survivor",
                    $"You pressed on {linker.CharacterLink(entity.Info)}!"));
                return;
            }

            GameManager.Instance.Notifications.DeleteNotification(transform.GetSiblingIndex());
        }
    }
}
