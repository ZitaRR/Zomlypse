using System;
using System.Collections.Generic;
using UnityEngine;
using Zomlypse.Behaviours;
using Zomlypse.Enums;
using Zomlypse.Extensions;

namespace Zomlypse.Singleton
{
    [Serializable]
    public class UI : Singleton
    {
        [SerializeField]
        private float speed;
        [SerializeField]
        private Transform root;

        private Dictionary<int, LTDescr> tweeningElements = new Dictionary<int, LTDescr>();
        private Component[] elements;

        public override void Initialize(GameManager _)
        {
            elements = new Component[root.childCount];
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = root.GetChild(i);
            }
        }

        public LTDescr Move(RectTransform rect, Vector3 position, float duration, Action action = null)
        {
            if (IsTweening(rect, out LTDescr ltd))
            {
                RemoveTween(ltd);
            }

            ltd = LeanTween.move(rect, position, duration);
            ltd.setOnStart(() => AddTween(ltd))
                .setOnComplete(() =>
                {
                    action?.Invoke();
                    RemoveTween(ltd);
                });
            return ltd;
        }

        public LTDescr Move(RectTransform rect, Vector3 position, Action action = null)
        {
            return Move(rect, position, speed, action);
        }

        public LTDescr Sweep(RectTransform child, RectTransform parent, Direction direction, float duration, Action action = null)
        {
            Vector3 position = parent.localPosition;
            switch (direction)
            {
                case Direction.Up:
                    position.y = parent.rect.yMax + (child.rect.height / 2);
                    break;
                case Direction.Right:
                    position.x = parent.offsetMax.x + (child.rect.width / 2);
                    break;
                case Direction.Down:
                    position.y = parent.rect.yMin - (child.rect.height / 2);
                    break;
                case Direction.Left:
                    position.x = parent.offsetMin.x - (child.rect.width / 2);
                    break;
            }

            return Move(child, position, duration, action)
                .setEase(LeanTweenType.easeInOutSine);
        }

        public LTDescr Sweep(RectTransform child, RectTransform parent, Direction direction, Action action = null)
        {
            return Sweep(child, parent, direction, speed, action);
        }

        public LTDescr SweepTransition(RectTransform child, RectTransform parent, Action action)
        {
            Direction current = parent.localPosition.DirectionTo(child.localPosition);
            return Sweep(child, parent, Direction.Normal, speed, () =>
            {
                action?.Invoke();
                Sweep(child, parent, current, speed);
            });
        }

        private void AddTween(LTDescr ltd)
        {
            if (IsTweening(ltd.rectTransform, out _))
            {
                return;
            }

            tweeningElements.Add(ltd.rectTransform.GetInstanceID(), ltd);
        }

        private bool RemoveTween(LTDescr ltd)
        {
            if (!tweeningElements.Remove(ltd.rectTransform.GetInstanceID()))
            {
                return false;
            }

            LeanTween.cancel(ltd.rectTransform);
            return true;
        }

        public bool IsTweening(RectTransform rect, out LTDescr ltd)
        {
            return tweeningElements.TryGetValue(rect.GetInstanceID(), out ltd);
        }

        public void Enable(string name)
        {
            SetElement(name, true);
        }

        public void Disable(string name)
        {
            SetElement(name, false);
        }

        public void DisableAll()
        {
            foreach (Component element in elements)
            {
                element.gameObject.SetActive(false);
            }
        }

        private void SetElement(string name, bool value)
        {
            foreach (Component element in elements)
            {
                if (element.name != name)
                {
                    continue;
                }

                element.gameObject.SetActive(value);
                return;
            }
        }
    }
}
