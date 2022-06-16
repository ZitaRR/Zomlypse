using System;
using UnityEngine;

public static class UI 
{
    public static LTDescr Sweep(RectTransform child, RectTransform parent, Direction direction, float duration = 1f, bool cancel = true)
    {
        if (cancel)
        {
            child.LeanCancel();
        }

        Vector3 position = parent.localPosition;
        switch (direction)
        {
            case Direction.Normal:
                position = parent.localPosition;
                break;
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

        return LeanTween.move(child, position, duration).setEase(LeanTweenType.easeInOutBack);
    }

    public static LTDescr SweepTransition(RectTransform child, RectTransform parent, Action action)
    {
        Direction current = parent.localPosition.DirectionTo(child.localPosition);
        LTDescr ltd = Sweep(child, parent, Direction.Normal, .8f, false).setOnComplete(() => action?.Invoke());
        return Sweep(child, parent, current, .8f, false).setDelay(ltd.time);
    }
}
