using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zomlypse.Interfaces;

namespace Zomlypse.Behaviours
{
    public class Notifications : MonoBehaviour
    {
        public float NotificationLifeTime { get => notificationLifeTime; }
        public float PromptLifeTime { get => promptLifeTime; }
        public INotification Current
        {
            get
            {
                if (notifications.Count > 0)
                {
                    return notifications[0];
                }
                return null;
            }
        }

        [SerializeField]
        private RectTransform notification;
        [SerializeField]
        private RectTransform prompt;
        [SerializeField]
        private float notificationLifeTime;
        [SerializeField]
        private float promptLifeTime;
        [SerializeField]
        private float spacing;
        [SerializeField]
        private float speed;

        private RectTransform view;
        private List<INotification> notifications = new List<INotification>();
        private Vector2 startPosition;
        private float offsetHeight;

        private void Awake()
        {
            view = GetComponent<RectTransform>();
        }

        private void Start()
        {
            startPosition = new Vector2(Screen.safeArea.xMin - view.rect.width, 0f);   
        }

        private void Update()
        {
            if (Current is null)
            {
                return;
            }

            Current.Update(Time.deltaTime);
            if (Current.HasExpired())
            {
                DeleteNotification();
            }
        }

        private IEnumerator AddNotification(INotification notification, RectTransform rect)
        {
            notification.Rect = Instantiate(rect, startPosition, Quaternion.identity, view);
            notifications.Add(notification);
            TextMeshProUGUI text = notification.Rect.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{notification.Header}\n{notification.Message}";

            yield return new WaitForEndOfFrame();

            float startY = view.rect.yMin + notification.Rect.rect.height / 2;
            notification.Rect.anchoredPosition = new Vector2(Screen.safeArea.xMin - view.rect.width, startY + offsetHeight);

            offsetHeight += notification.Rect.rect.height + spacing;
            UI.Move(notification.Rect, new Vector3(
                notification.Rect.anchoredPosition.x + view.rect.width,
                notification.Rect.anchoredPosition.y),
                speed);
        }

        private INotification PopAt(int index)
        {
            if (notifications.Count == 0)
            {
                return null;
            }

            INotification notification = notifications[index];
            notifications.RemoveAt(index);
            offsetHeight -= notification.Rect.rect.height + spacing;
            AdjustNotifications();

            return notification;
        }

        public void DeleteNotification(int index = 0)
        {
            INotification notification = PopAt(index);
            if (notification is null)
            {
                return;
            }
            notification.Delete();
        }

        public void DeletePrompt(int index = 0, bool success = false)
        {
            if (!(PopAt(index) is Prompt prompt))
            {
                return;
            }

            prompt.Success = success;
            prompt.Delete();
        }

        private void AdjustNotifications()
        {
            if (notifications.Count <= 0)
            {
                return;
            }

            Vector2 target = new Vector2(startPosition.x + view.rect.width, view.rect.yMin);
            for (int i = 0; i < notifications.Count; i++)
            {
                RectTransform rect = notifications[i].Rect;
                target.x = rect.anchoredPosition.x;
                target.y += rect.rect.height / 2;

                if (UI.IsTweening(rect, out _))
                {
                    target = new Vector2(view.anchoredPosition.x - rect.rect.width / 2, target.y);
                }

                UI.Move(rect, target, speed);
                target.y += rect.rect.yMax + spacing;
            }
        }

        public void Add(Notification notification)
        {
            StartCoroutine(AddNotification(notification, this.notification));
        }

        public void Add(Prompt prompt)
        {
            StartCoroutine(AddNotification(prompt, this.prompt));
        }
    }
}
