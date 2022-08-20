using UnityEngine;
using UnityEngine.EventSystems;
using Zomlypse.Behaviours;

namespace Zomlypse.Helpers
{
    public class NotificationClose : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.Instance.Notifications.DeleteNotification(transform.GetSiblingIndex());
        }
    }
}
