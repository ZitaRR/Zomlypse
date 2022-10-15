using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zomlypse.Behaviours;

namespace Zomlypse.Helpers
{
    public class NotificationClose : MonoBehaviour, IPointerClickHandler
    {
        private TextMeshProUGUI text;

        private void Start()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int index = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (index > -1)
            {
                TMP_LinkInfo info = text.textInfo.linkInfo[index];
                string content = info.GetLinkText();
                GameManager.Instance.Notifications.Add(new Notification(
                    "Survivor",
                    $"This is <color=red>{content}</color>"));
                return;
            }

            GameManager.Instance.Notifications.DeleteNotification(transform.GetSiblingIndex());
        }
    }
}
