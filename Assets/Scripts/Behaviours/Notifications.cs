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
                time = 0f;
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

            RectTransform rect = notifications[index];
            notifications.RemoveAt(index);
            Destroy(rect.gameObject);

            offsetHeight -= rect.rect.height + spacing;
            AdjustNotifications(rect.rect.height, index);
        }

        private void AdjustNotifications(float previousHeight, int index)
        {
            if (notifications.Count <= 0)
            {
                return;
            }

            for (int i = index; i < notifications.Count; i++)
            {
                RectTransform rect = notifications[i];
                Vector2 target = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y - previousHeight - spacing);

                if (i == 0)
                {
                    UI.Move(rect, target, speed);
                    continue;
                }
                else if (UI.IsTweening(rect, out _))
                {
                    UI.Move(rect, new Vector3(view.anchoredPosition.x - rect.rect.width / 2, target.y), speed);
                    continue;
                }

                UI.Move(rect, target, speed);
            }

            /*foreach (RectTransform rect in notifications)
            {
                Vector2 target = new Vector2(rect.anchoredPosition.x,  rect.anchoredPosition.y - previousHeight - spacing);
                if (++index == 1)
                {
                    UI.Move(rect, target, speed, () => DeleteNotification());
                    continue;
                }
                else if (UI.IsTweening(rect, out _))
                {
                    UI.Move(rect, new Vector3(view.anchoredPosition.x - rect.rect.width / 2, target.y), speed);
                    continue;
                }

                UI.Move(rect, target, speed);
            }*/
        }

        public void Add(string header, string description)
        {
            StartCoroutine(AddNotification(header, description));
        }
    }
}
