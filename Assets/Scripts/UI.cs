using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zomlypse.Enums;
using Zomlypse.Extensions;

namespace Zomlypse
{
    public static class UI
    {
        private const float DEFAULT_SPEED = .6f;

        private static Dictionary<int, LTDescr> tweeningElements = new Dictionary<int, LTDescr>();
        private static List<Component> elements = new List<Component>();

        public static void Initialize(params Component[] components)
        {
            elements.AddRange(components);
        }

        public static LTDescr Move(RectTransform rect, Vector3 position, float duration = 1f, Action action = null)
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

        public static LTDescr Sweep(RectTransform child, RectTransform parent, Direction direction, float duration = DEFAULT_SPEED, Action action = null)
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

        public static LTDescr SweepTransition(RectTransform child, RectTransform parent, Action action)
        {
            Direction current = parent.localPosition.DirectionTo(child.localPosition);
            return Sweep(child, parent, Direction.Normal, DEFAULT_SPEED, () =>
            {
                action?.Invoke();
                Sweep(child, parent, current, DEFAULT_SPEED);
            });
        }

        private static void AddTween(LTDescr ltd)
        {
            if (IsTweening(ltd.rectTransform, out _))
            {
                return;
            }

            tweeningElements.Add(ltd.rectTransform.GetInstanceID(), ltd);
        }

        private static bool RemoveTween(LTDescr ltd)
        {
            if (!tweeningElements.Remove(ltd.rectTransform.GetInstanceID()))
            {
                return false;
            }

            LeanTween.cancel(ltd.rectTransform);
            return true;
        }

        public static bool IsTweening(RectTransform rect, out LTDescr ltd)
        {
            return tweeningElements.TryGetValue(rect.GetInstanceID(), out ltd);
        }

        public static void Enable(string name)
        {
            SetElement(name, true);
        }

        public static void Disable(string name)
        {
            SetElement(name, false);
        }

        public static void DisableAll()
        {
            foreach (Component element in elements)
            {
                element.gameObject.SetActive(false);
            }
        }

        private static void SetElement(string name, bool value)
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
