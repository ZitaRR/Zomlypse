using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Zomlypse.Behaviours
{
    public class Notifications : MonoBehaviour
    {
        [SerializeField]
        private RectTransform notification;
        [SerializeField]
        private float lifeTime;
        [SerializeField]
        private float spacing;

        private RectTransform view;
        private Queue<RectTransform> notifications = new Queue<RectTransform>();
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

        private IEnumerator AddNotification(string header, string description)
        {
            RectTransform rect = Instantiate(notification, startPosition, Quaternion.identity, view);
            notifications.Enqueue(rect);
            TextMeshProUGUI text = rect.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{header}\n{description}";

            yield return new WaitForEndOfFrame();

            float startY = view.rect.yMin + rect.rect.height / 2;
            rect.anchoredPosition = new Vector2(Screen.safeArea.xMin - view.rect.width, startY + offsetHeight);

            offsetHeight += rect.rect.height + spacing;
            int temp = notifications.Count;
            UI.Move(rect, new Vector3(rect.anchoredPosition.x + view.rect.width, rect.anchoredPosition.y), action: () =>
            {
                if (temp == 1)
                {
                    StartCoroutine(DeleteNotification());
                }
            });
        }

        private IEnumerator DeleteNotification()
        {
            if (notifications.Count <= 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(lifeTime);

            RectTransform rect = notifications.Dequeue();
            Destroy(rect.gameObject);

            offsetHeight -= rect.rect.height + spacing;
            AdjustNotifications(rect.rect.height);
        }

        private void AdjustNotifications(float previousHeight)
        {
            if (notifications.Count <= 0)
            {
                return;
            }

            int index = 0;
            foreach (RectTransform rect in notifications)
            {
                Vector2 target = new Vector2(rect.anchoredPosition.x,  rect.anchoredPosition.y - previousHeight - spacing);
                if (++index == 1)
                {
                    UI.Move(rect, target, action: () => StartCoroutine(DeleteNotification()));
                    continue;
                }
                else if (UI.IsTweening(rect, out _))
                {
                    UI.Move(rect, new Vector3(view.anchoredPosition.x - rect.rect.width / 2, target.y));
                    continue;
                }

                UI.Move(rect, target);
            }
        }

        public void Add(string header, string description)
        {
            StartCoroutine(AddNotification(header, description));
        }
    }
}
