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

            float startY = view.rect.yMin + rect.rect.height / 2 + rect.rect.height * (notifications.Count - 1) + (spacing * notifications.Count - 1);
            rect.anchoredPosition = new Vector2(Screen.safeArea.xMin - view.rect.width, startY);

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

            AdjustNotifications(index);
        }

        private void AdjustNotifications(int index)
        {
            if (notifications.Count <= 0)
            {
                return;
            }

            Vector2 target = new Vector2(startPosition.x + view.rect.width, view.rect.yMin);
            for (int i = index; i < notifications.Count; i++)
            {
                RectTransform rect = notifications[i];
                target.x = rect.anchoredPosition.x;
                target.y = view.rect.yMin + rect.rect.height / 2 + rect.rect.height * i + (spacing * i);

                if (UI.IsTweening(rect, out LTDescr ltd))
                {
                    UI.Move(rect, new Vector3(view.anchoredPosition.x - rect.rect.width / 2, target.y), speed);
                    continue;
                }

                UI.Move(rect, target, speed);
            }
        }

        public void Add(string header, string description)
        {
            StartCoroutine(AddNotification(header, description));
        }
    }
}
