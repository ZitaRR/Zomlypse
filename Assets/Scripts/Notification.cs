using UnityEngine;
using Zomlypse.Interfaces;
using Zomlypse.Singletons;

namespace Zomlypse
{
    public class Notification : INotification
    {
        public string Header { get; }
        public string Message { get; }
        public RectTransform Rect { get; set; }
        public float Time { get; private set; }

        public Notification(string header, string message)
        {
            Header = header;
            Message = message;
        }

        public void Update(float deltaTime)
        {
            Time += deltaTime;
        }

        public bool HasExpired()
        {
            return Time >= GameManager.Instance.Notifications.NotificationLifeTime;
        }

        public void Delete()
        {
            Object.Destroy(Rect.gameObject);
        }
    }
}
