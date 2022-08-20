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
        [SerializeField]
        private float speed;

        private RectTransform view;
        private List<RectTransform> notifications = new List<RectTransform>();
        private Vector2 startPosition;
        private float offsetHeight;
        private float time;

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
            if (notifications.Count == 0)
            {
                time = 0f;
                return;
            }

            time += Time.deltaTime;
            if (time >= lifeTime)
            {
                DeleteNotification();
            }
        }

        private IEnumerator AddNotification(string header, string description)
        {
            RectTransform rect = Instantiate(notification, startPosition, Quaternion.identity, view);
            notifications.Add(rect);
            TextMeshProUGUI text = rect.GetComponentInChildren<TextMeshProUGUI>();
            text.text = $"{header}\n{description}";

            yield return new WaitForEndOfFrame();

            float startY = view.rect.yMin + rect.rect.height / 2;
            rect.anchoredPosition = new Vector2(Screen.safeArea.xMin - view.rect.width, startY + offsetHeight);

            offsetHeight += rect.rect.height + spacing;
            UI.Move(rect, new Vector3(rect.anchoredPosition.x + view.rect.width, rect.anchoredPosition.y), speed);
        }

        public void DeleteNotification(int index = 0)
        {
            if (notifications.Count == 0)
            {
                return;
            }
            if (index == 0)
            {
                time = 0f;
            }

            RectTransform rect = notifications[index];
            notifications.RemoveAt(index);
            Destroy(rect.gameObject);

            offsetHeight -= rect.rect.height + spacing;
            AdjustNotifications();
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
                RectTransform rect = notifications[i];
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

        public void Add(string header, string description)
        {
            StartCoroutine(AddNotification(header, description));
        }
    }
}
